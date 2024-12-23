using System.Collections.Immutable;
using Appdiv.Payment.Telebirr.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;
//using SoapCore;
// using CoreWCF;
// using CoreWCF.Configuration;

namespace Appdiv.Payment.Telebirr;

public static class Startup
{
    public static IServiceCollection AddTelebirr(this IServiceCollection services)
    {

        services.AddSoapCore();
        return services
            .AddSingleton<ITelebirrPayment, TelebirrPayment>()
            .AddSingleton<ITelebirrService, TelebirrService>();
        
    }
    public static IApplicationBuilder UseTelebirr(this IApplicationBuilder builder, string endpoint = "/Telebirr.asmx")
    {
        return UseTelebirr<TelebirrCustomMessage>(builder, endpoint);
    }
    
    public static IApplicationBuilder UseTelebirr<T>(this IApplicationBuilder builder, string endpoint = "/Telebirr.asmx") where T : CustomMessage, new()
    {
        builder.UseSoapEndpoint<ITelebirrService, T>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
        return builder;
    }


}
