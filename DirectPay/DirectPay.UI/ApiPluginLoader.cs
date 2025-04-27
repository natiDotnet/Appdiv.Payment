using System.Reflection;
using System.Runtime.Loader;

namespace DirectPay.UI;

public interface IApiPlugin
{
    void MapEndpoints(IEndpointRouteBuilder app);
}
public class ApiPluginLoader
{
    private readonly string _pluginPath;

    public ApiPluginLoader(string pluginPath)
    {
        _pluginPath = pluginPath;
    }

    public IEnumerable<IApiPlugin> LoadApiPlugins()
    {
        var plugins = new List<IApiPlugin>();

        foreach (var dll in Directory.GetFiles(_pluginPath, "*.dll"))
        {
            var alc = new AssemblyLoadContext(Path.GetFileNameWithoutExtension(dll), true);

            using var stream = new FileStream(dll, FileMode.Open, FileAccess.Read);
            var assembly = alc.LoadFromStream(stream);

            var pluginTypes = assembly.GetTypes()
                .Where(t => typeof(IApiPlugin).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var type in pluginTypes)
            {
                if (Activator.CreateInstance(type) is IApiPlugin instance)
                {
                    plugins.Add(instance);
                }
            }
        }

        return plugins;
    }
}
