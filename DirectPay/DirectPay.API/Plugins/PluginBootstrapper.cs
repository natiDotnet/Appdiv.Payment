using System.Reflection;
using DirectPay.Application.Abstration;
using Serilog;

namespace DirectPay.API.Plugins;
public static class PluginBootstrapper
{
    public static IEnumerable<PluginStartup> PluginStartups = [];
    public static IEnumerable<Assembly> LoadAssemblies(string basePath)
    {
        // Get all plugin directories
        var pluginDirs = Directory.GetDirectories(basePath);

        foreach (var pluginDir in pluginDirs)
        {
            // Search for DLLs recursively in each plugin directory
            return Directory.GetFiles(pluginDir, "*.dll", SearchOption.AllDirectories)
            .Where(dllPath =>
            {
                string directoryName = Path.GetFileName(Path.GetDirectoryName(dllPath));
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(dllPath);
                return string.Equals(directoryName, fileNameWithoutExtension, StringComparison.OrdinalIgnoreCase);
            }).Select(Assembly.LoadFrom);

            // foreach (var dll in dlls)
            // {
            //     Log.Information("Loaded plugin: {PluginPath}", dll);
            //     yield return Assembly.LoadFrom(dll);
            // }
        }
        return [];
    }

    public static void ApplyConfigureServices(IServiceCollection services, IConfiguration config, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (PluginStartup startup in GetPluginStartup(assembly))
            {
                try
                {
                    startup.AddPlugin(services, config);
                    Log.Information("Configured services from {@Method}", startup.Name);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to invoke ConfigureServices in {@Method}", startup.Name);
                }
            }
        }
    }

    public static void ApplyConfigureMiddleware(IApplicationBuilder app, IConfiguration configuration, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (PluginStartup startup in GetPluginStartup(assembly))
            {
                try
                {
                    startup.UsePlugin(app, configuration);
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
