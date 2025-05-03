using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Abstaction;
using DirectPay.Application.Abstration;

namespace DirectPay.Telebirr;
public class Startup : PluginStartup
{

    public override string Name => "Telebirr";

    public override string Description => "Telebirr Payment";

    public override string Version => "1.0.0";

    public override IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTelebirr<TelebirrPayment>();
        return services;
    }

    public override IApplicationBuilder UsePlugin(IApplicationBuilder app)
    {
        app.UseTelebirr();
        return app;
    }
}