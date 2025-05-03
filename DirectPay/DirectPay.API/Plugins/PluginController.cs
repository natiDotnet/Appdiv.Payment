using Microsoft.AspNetCore.Mvc;

namespace DirectPay.API.Plugins;
[ApiController]
[Route("api/[controller]")]
public class PluginController : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> GetPlugins()
    {
        var plugins = PluginBootstrapper.PluginStartups.Select(p => p.Name).ToArray(); // Get the number of plugins loaded by the ap
        return plugins; // Return the number of plugins loaded by the ap
    }
}
