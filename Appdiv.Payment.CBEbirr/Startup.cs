using Microsoft.AspNetCore.Builder;
using SoapCore;
using Microsoft.Extensions.DependencyInjection;
using Appdiv.C2BPayment.CBEbirr.Services;
using Appdiv.Payment.Telebirr;

namespace Appdiv.C2BPayment.CBEbirr;

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
        // builder.UseSoapEndpoint<ITelebirrService, CBEbirrCustomMessage>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
        // builder.UseSoapEndpoint<ICBEbirrService, CBEbirrCustomMessage>(endpoint, new SoapEncoderOptions() { MessageVersion = MessageVersion.Soap12WSAddressingAugust2004 }, SoapSerializer.XmlSerializer);
        //builder.UseSoapEndpoint<CBEbirrService, CBEbirrCustomMessage>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
        return builder;
    }


}
