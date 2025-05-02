using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Abstaction;

namespace DirectPay.Telebirr;
[PluginMetadata("Telebirr", "1.0.0")]
public static class Startup
{
    [PluginConfigureServices]
    public static IServiceCollection AddPlugin(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTelebirr<TelebirrPayment>();
        return services;
    }
    [PluginConfigureMiddleware]
    public static IApplicationBuilder UsePlugin(this IApplicationBuilder app)
    {
        app.UseTelebirr();
        return app;
    }
}