using System.Reflection;
using DirectPay.Application.Abstration;
using Serilog;

namespace DirectPay.API.Plugins;
public static class PluginBootstrapper
{
    public static IEnumerable<PluginStartup> PluginStartups = [];
    public static IEnumerable<Assembly> RouteAssemblies = PluginStartups.SelectMany(p => p.GetRazorComponents().Select(v => v.Component.Assembly));
    public static IEnumerable<PluginView> PluginViews = PluginStartups.SelectMany(p => p.GetRazorComponents());

    public static IEnumerable<Assembly> LoadAssemblies(string basePath)
    {
        // Get all plugin directories
        var pluginDirs = Directory.GetDirectories(basePath);

        // Search for DLLs recursively in each plugin directory
        return pluginDirs.SelectMany(pluginDir =>
        Directory.GetFiles(pluginDir, "*.dll", SearchOption.AllDirectories)
        .Where(dllPath =>
        {
            Console.WriteLine(dllPath);
            string directoryName = Path.GetFileName(Path.GetDirectoryName(dllPath));
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dllPath);
            return string.Equals(directoryName, fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase);
        }).Select(Assembly.LoadFrom)
        .Where(s => s != null));
    }

    public static async Task ApplyConfigureServices(IServiceCollection services, IConfiguration config, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (PluginStartup startup in GetPluginStartup(assembly))
            {
                try
                {
                    startup.AddPlugin(services, config);
                    await startup.AddPluginAsync(services, config);
                    Log.Information("Configured services from {@Method}", startup.Name);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to invoke ConfigureServices in {@Method}", startup.Name);
                }
            }
        }
    }

    public static async Task ApplyConfigureMiddleware(IApplicationBuilder app, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (PluginStartup startup in GetPluginStartup(assembly))
            {
                try
                {
                    // var setting = app.ApplicationServices.GetRequiredService<ISettingRepository>();
                    // Console.WriteLine(setting == null);
                    startup.UsePlugin(app);
                    startup.UsePlugin(app, configuration);
                    await startup.UsePluginAsync(app);
                    await startup.UsePluginAsync(app, configuration);
                    Log.Information("Configured middleware from {@Method}", startup.Name);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to invoke ConfigureMiddleware in {@Method}", startup.Name);
                }
            }
        }
    }

    private static IEnumerable<MethodInfo> GetMethodsWithAttribute<T>(Assembly assembly)
        where T : Attribute =>
        assembly.GetTypes()
                .Where(t => t.IsClass && t.IsAbstract && t.IsSealed) // static class
                .SelectMany(t => t.GetMethods(BindingFlags.Public | BindingFlags.Static))
                .Where(m => m.GetCustomAttribute<T>() != null);

    public static IEnumerable<PluginStartup> GetPluginStartup(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(t => typeof(PluginStartup).IsAssignableFrom(t) && !t.IsAbstract)
            .Select(t => Activator.CreateInstance(t) as PluginStartup)
            .Where(p => p is not null)!;
    }
}
