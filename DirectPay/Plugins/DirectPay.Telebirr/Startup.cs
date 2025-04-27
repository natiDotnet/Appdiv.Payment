using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.Telebirr;

namespace DirectPay.Telebirr;

public static class Startup
{
    public static IServiceCollection AddPlugin(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTelebirr<TelebirrPayment>();
        return services;
    }

    public static IApplicationBuilder UsePlugin(this IApplicationBuilder app)
    {
        app.UseTelebirr();
        return app;
    }
}