using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.CBEBirr;
using DirectPay.Application.Abstaction;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Cbebirr;
[PluginMetadata("CbeBirr", "1.0.0")]
public class Startup
{
    [PluginConfigureServices]
    public static IServiceCollection AddCbeBirr(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCBEBirr<CBEbirrPayment>();
        return services;
    }
    [PluginConfigureMiddleware]
    public static IApplicationBuilder UseCbeBirr(IApplicationBuilder app)
    {
        app.UseCBEBirr();
        return app;
    }
}