using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Appdiv.Payment.CBEBirr;
using DirectPay.Application.Abstaction;
using DirectPay.Application.Abstration;
using DirectPay.Cbebirr.Payments;
using DirectPay.Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Cbebirr;
[PluginMetadata("CbeBirr", "1.0.0")]
public class Startup : PluginStartup
{
    public override string Name => "CbeBirr";

    public override string Description => "CbeBirr Payment";

    public override string Version => "1.0.0";

    public override IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCBEBirr<CBEbirrPayment>();
        return services;
    }
    public override async Task<IApplicationBuilder> UsePluginAsync(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var settings = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
        var setting = await settings.ReadByKey("CbeCallback");
        if (setting == null)
        {
            setting = new Setting
            {
                Key = "CbeCallback",
                Configuration = JsonSerializer.Serialize(new CbeOptions())
            };
            await settings.AddAsync(setting);
        }

        CbeOptions cbe = JsonSerializer.Deserialize<CbeOptions>(setting.Configuration)!;
        // app.UseCBEBirr();
        app.UseEndpoints(e =>
        {
            e.MapControllers();
            e.Endpoint(cbe);
        });
        app.UseCBEBirr(
            endpoint: $"/{cbe.BasePath}",
            paymentConfirmationPath: $"/{cbe.PaymentConfirmationPath}",
            paymentQueryPath: $"/{cbe.PaymentQueryPath}",
            paymentValidationPath: $"/{cbe.PaymentValidationPath}"
        );
        return app;
    }
}