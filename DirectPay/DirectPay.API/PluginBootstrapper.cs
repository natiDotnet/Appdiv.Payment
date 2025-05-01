using System.Reflection;
using System.Runtime.Loader;
using DirectPay.Application.Abstaction;
using Serilog;

namespace DirectPay.API;
public static class PluginBootstrapper
{
    public static IEnumerable<Assembly> LoadAssemblies(string basePath)
    {
        // Get all plugin directories
        var pluginDirs = Directory.GetDirectories(basePath);

        foreach (var pluginDir in pluginDirs)
        {
            // Search for DLLs recursively in each plugin directory
            var dlls = Directory.GetFiles(pluginDir, "*.dll", SearchOption.AllDirectories);

            foreach (var dll in dlls)
            {
                var alc = new AssemblyLoadContext(Path.GetFileNameWithoutExtension(dll), true);
                using var stream = File.OpenRead(dll);
                yield return alc.LoadFromStream(stream);
                Log.Information("Loaded plugin: {PluginPath}", dll);
            }
        }
    }

    public static void ApplyConfigureServices(IServiceCollection services, IConfiguration config, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var method in GetMethodsWithAttribute<PluginConfigureServicesAttribute>(assembly))
            {
                try
                {
                    method.Invoke(null, [services, config]);
                    Log.Information("Configured services from {@Method}", method.DeclaringType?.FullName);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to invoke ConfigureServices in {@Method}", method.DeclaringType?.FullName);
                }
            }
        }
    }

    public static void ApplyConfigureMiddleware(IApplicationBuilder app, IEnumerable<Assembly> assemblies)
    {
        foreach (var assembly in assemblies)
        {
            foreach (var method in GetMethodsWithAttribute<PluginConfigureMiddlewareAttribute>(assembly))
            {
                try
                {
                    method.Invoke(null, [app]);
                    Log.Information("Configured middleware from {@Method}", method.DeclaringType?.FullName);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Failed to invoke ConfigureMiddleware in {@Method}", method.DeclaringType?.FullName);
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
}
