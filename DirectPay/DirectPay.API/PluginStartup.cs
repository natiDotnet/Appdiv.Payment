using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DirectPay.API;

public class PluginStartup
{

    public static void RegisterPlugin(WebApplicationBuilder builder)
    {
        // Register plugin assemblies
        var mvcBuilder = builder.Services.AddControllers()
            .ConfigureApplicationPartManager(apm =>
            {
                var pluginDlls = Directory.GetFiles("Plugins", "*.dll");
                foreach (var dll in pluginDlls)
                {
                    var asm = Assembly.LoadFrom(dll);
                    apm.ApplicationParts.Add(new AssemblyPart(asm));
                }
            });
    }
}
