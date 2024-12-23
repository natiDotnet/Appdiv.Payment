using System.ServiceModel.Channels;
using Appdiv.Payment.CBEbirr.Services;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Appdiv.Payment.CBEbirr;

public static class Startup
{
    public static IServiceCollection AddCBEbirr(this IServiceCollection services)
    {
        return services.AddSoapCore()
            .AddTelebirr()
            .AddSingleton<ICBEbirrPayment, CBEbirrPayment>()
            .AddSingleton<ICBEbirrService, CBEbirrService>();

    }
    public static IApplicationBuilder UseCBEbirr(this IApplicationBuilder builder, string endpoint = "/cbebirr.asmx")
    {
        builder.UseTelebirr<CBEbirrCustomMessage>(endpoint);
        builder.UseSoapEndpoint<ICBEbirrService, CBEbirrCustomMessage>(endpoint,
            new SoapEncoderOptions()
            {
                MessageVersion = MessageVersion.Soap12WSAddressingAugust2004
            },
            SoapSerializer.XmlSerializer);
        return builder;
    }


}
