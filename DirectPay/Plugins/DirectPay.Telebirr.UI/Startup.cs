using DirectPay.Application.Abstration;

namespace DirectPay.Telebirr.UI;

public class Startup : PluginStartup
{
    public override string Name => "Telebirr UI";
    public override string Description => "Telebirr UI Plugin";
    public override string Version => "1.0.0";

    public override IEnumerable<PluginView> GetRazorComponents()
    {
        return [
            new PluginView
            {
                Title = "Telebirr",
                Component = typeof(Component1)
            }
        ];
    }
}