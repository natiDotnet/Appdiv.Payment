using System.ServiceModel.Channels;
using Appdiv.Payment.CBEbirr.Services;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Appdiv.Payment.CBEbirr;

public static class Startup
{
    public static IServiceCollection AddCBEbirr<TImplementation>(this IServiceCollection services)
        where TImplementation : class, ISharedPayment, ICBEbirrPayment
    {
        return services.AddSoapCore()
            .AddSingleton<ICBEbirrPayment, TImplementation>()
            .AddSingleton<ICBEbirrService, CBEbirrService>()
            .AddSingleton<ICBESharedService, CBEbirrService>();
    }

    public static IApplicationBuilder UseCBEbirr(
        this IApplicationBuilder builder,
        string endpoint = "/cbebirr",
        string paymentQueryPath = "/paymentQuery",
        string paymentValidationPath = "/paymentValidation",
        string paymentConfirmationPath = "/paymentConfirmation")
    {
        builder.UseSoapEndpoint<ICBEbirrService, CBETransactionMessage>($"{endpoint}{paymentQueryPath}",
            new SoapEncoderOptions
            {
                MessageVersion = MessageVersion.Soap12WSAddressingAugust2004
            },
            SoapSerializer.XmlSerializer);
        builder.UseTelebirr<ICBESharedService, CBEbirrMessage>(endpoint, "/Query", paymentValidationPath,
            paymentConfirmationPath);
        return builder;
    }
}