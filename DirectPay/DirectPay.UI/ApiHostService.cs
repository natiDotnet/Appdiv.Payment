using DirectPay.API.Services;
using DirectPay.API.Transactions;
using DirectPay.Application;
using DirectPay.UI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;

namespace DirectPay.UI;
public class ApiHostService : BackgroundService
{
    private WebApplication? _app;
    private readonly IServiceProvider _services;
    private readonly ILogger<ApiHostService> _logger;
    private readonly string _pluginPath;

    public ApiHostService(IServiceProvider services, ILogger<ApiHostService> logger, IConfiguration config)
    {
        _services = services;
        _logger = logger;
        _pluginPath = Path.Combine(AppContext.BaseDirectory, "Plugins");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        StartApp();

        using var watcher = new FileSystemWatcher(_pluginPath, "*.dll")
        {
            EnableRaisingEvents = true,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
        };

        watcher.Created += async (s, e) =>
        {
            _logger.LogInformation("New plugin detected: {Plugin}", e.Name);
            await RestartApp();
        };

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private void StartApp()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls("http://localhost:2122", "https://localhost:7172");

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddMudServices();
        builder.Services.AddSingleton<WeatherForecastService>();
        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddControllers()
            .PartManager
            .ApplicationParts
            .Add(new AssemblyPart(typeof(Handler).Assembly));


        var app = builder.Build();

        // var pluginLoader = new ApiPluginLoader(_pluginPath);
        // var plugins = pluginLoader.LoadApiPlugins();
        // foreach (var plugin in plugins)
        // {
        //     plugin.MapEndpoints(app);
        // }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "DirectPay API V1");
            c.RoutePrefix = "docs"; // Changed from "docs" to the standard "swagger"
        });

        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapControllers();

        // Register DirectPay API endpoints
        // Handler.Endpoint(app);

        app.MapFallbackToPage("/_Host");

        _ = Task.Run(() => app.RunAsync());
        _app = app;
    }

    private async Task RestartApp()
    {
        await _app?.StopAsync()!;
        // await _app.StartAsync();
        StartApp();
    }
}
