using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Abstration;
using DirectPay.Application.Abstration;
using DirectPay.Domain.Settings;
using DirectPay.Telebirr.Payment;
using DirectPay.Telebirr.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor;
using MudBlazor.Services;

namespace DirectPay.Telebirr;
public class Startup : PluginStartup
{

    public override string Name => "Telebirr";

    public override string Description => "Telebirr Payment";

    public override string Version => "1.0.0";

    public override string Icon => "fas fa-home";

    public override IEnumerable<PluginView> GetRazorComponents()
    {
        return
        [
            new PluginView
            {
                Title = "Settings",
                Component = typeof(Settings),
                Icon = Icons.Material.Filled.Settings,
            }
        ];
    }

    public override IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTelebirr<TelebirrPayment>();
        services.Configure<TelebirrOptions>(configuration.GetSection("Telebirr"));
        // services.AddMudServices();
        // services.AddControllers()
        //     .PartManager
        //     .ApplicationParts
        //     .Add(new AssemblyPart(typeof(TelebirrController).Assembly));
        return services;
    }

    public override async Task<IApplicationBuilder> UsePluginAsync(IApplicationBuilder app, IConfiguration configuration)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var settings = scope.ServiceProvider.GetRequiredService<ISettingRepository>();
        var setting = await settings.ReadByKey("TelebirrCallback");
        if (setting == null)
        {
            setting = new Setting
            {
                Key = "TelebirrCallback",
                Configuration = JsonSerializer.Serialize(new TelebirrOptions())
            };
            await settings.AddAsync(setting);
        }

        TelebirrOptions telebirr = JsonSerializer.Deserialize<TelebirrOptions>(setting.Configuration)!;
        // TelebirrOptions telebirr = configuration.GetSection("Telebirr").Get<TelebirrOptions>()!;
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.Endpoints(telebirr);
        });

        app.UseTelebirr(
            endpoint: $"/{telebirr.BasePath}",
            paymentConfirmationPath: $"/{telebirr.PaymentConfirmationPath}",
            paymentQueryPath: $"/{telebirr.PaymentQueryPath}",
            paymentValidationPath: $"/{telebirr.PaymentValidationPath}"
        );
        return app;
    }
}