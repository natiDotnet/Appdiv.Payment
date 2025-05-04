using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Abstaction;
using DirectPay.Application.Abstration;
using DirectPay.Telebirr.Payment;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace DirectPay.Telebirr;
public class Startup : PluginStartup
{

    public override string Name => "Telebirr";

    public override string Description => "Telebirr Payment";

    public override string Version => "1.0.0";

    public override IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTelebirr<TelebirrPayment>();
        services.Configure<TelebirrOptions>(configuration.GetSection("Telebirr"));
        // services.AddControllers()
        //     .PartManager
        //     .ApplicationParts
        //     .Add(new AssemblyPart(typeof(TelebirrController).Assembly));
        return services;
    }

    public override IApplicationBuilder UsePlugin(IApplicationBuilder app, IConfiguration configuration)
    {
        var test = configuration["Telebirr"];
        TelebirrOptions telebirr = configuration.GetSection("Telebirr").Get<TelebirrOptions>()!;
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.Endpoints(telebirr);
        });

        app.UseTelebirr(
            endpoint: telebirr.BasePath,
            paymentConfirmationPath: telebirr.PaymentConfirmationPath,
            paymentQueryPath: telebirr.PaymentQueryPath,
            paymentValidationPath: telebirr.PaymentValidationPath);
        return app;
    }
}