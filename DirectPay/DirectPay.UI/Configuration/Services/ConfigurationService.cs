using System.Text.Json;

namespace DirectPay.UI.Configuration.Services;

public interface IConfigurationService
{
    Task UpdateSection<T>(string sectionName, T sectionData) where T : class;
    Task<T?> GetSection<T>(string sectionName) where T : class;
}

public class ConfigurationService : IConfigurationService
{
    private readonly IWebHostEnvironment _environment;
    private readonly string _configPath;
    private static readonly JsonSerializerOptions _jsonOptions = new() { WriteIndented = true };

    public ConfigurationService(IWebHostEnvironment environment)
    {
        _environment = environment;
        _configPath = Path.Combine(_environment.ContentRootPath, "appsettings.json");
    }

    public async Task UpdateSection<T>(string sectionName, T sectionData) where T : class
    {
        var jsonString = await File.ReadAllTextAsync(_configPath);
        var existingConfig = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonString);
        existingConfig![sectionName] = JsonSerializer.SerializeToElement(sectionData);
        await File.WriteAllTextAsync(_configPath, JsonSerializer.Serialize(existingConfig, _jsonOptions));
    }

    public async Task<T?> GetSection<T>(string sectionName) where T : class
    {
        var jsonString = await File.ReadAllTextAsync(_configPath);
        var jsonDoc = JsonDocument.Parse(jsonString);

        if (jsonDoc.RootElement.TryGetProperty(sectionName, out var section))
        {
            return JsonSerializer.Deserialize<T>(section.GetRawText());
        }

        return null;
    }
}