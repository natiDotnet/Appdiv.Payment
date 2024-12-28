using System.ServiceModel.Channels;
using Appdiv.Payment.CBE.Services;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Appdiv.Payment.CBE;

public static class Startup
{
    public static IServiceCollection AddCBEbirr<TImplementation>(this IServiceCollection services)
        where TImplementation : class, ISharedPayment, ICBEPayment
    {
        return services.AddSoapCore()
            .AddSingleton<ICBEPayment, TImplementation>()
            .AddSingleton<ICBEService, CBEService>()
            .AddSingleton<ICBESharedService, CBEService>();
    }

    public static IApplicationBuilder UseCBEbirr(
        this IApplicationBuilder builder,
        string endpoint = "/cbebirr",
        string paymentQueryPath = "/paymentQuery",
        string paymentValidationPath = "/paymentValidation",
        string paymentConfirmationPath = "/paymentConfirmation")
    {
        builder.UseSoapEndpoint<ICBEService, CBETransactionMessage>($"{endpoint}{paymentQueryPath}",
            new SoapEncoderOptions
            {
                MessageVersion = MessageVersion.Soap12WSAddressingAugust2004
            },
            SoapSerializer.XmlSerializer);
        builder.UseTelebirr<ICBESharedService, CBEMessage>(endpoint, "/Query", paymentValidationPath,
            paymentConfirmationPath);
        return builder;
    }
}