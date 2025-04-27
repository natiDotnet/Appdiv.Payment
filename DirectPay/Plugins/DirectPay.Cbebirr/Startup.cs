using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appdiv.Payment.CBEBirr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Cbebirr;

public class Startup
{
    public static IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCBEBirr<CBEbirrPayment>();
        return services;
    }

    public static IApplicationBuilder UsePlugin(IApplicationBuilder app)
    {
        app.UseCBEBirr();
        return app;
    }
}