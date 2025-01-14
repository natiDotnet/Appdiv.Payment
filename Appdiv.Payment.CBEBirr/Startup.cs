using System.ServiceModel.Channels;
using Appdiv.Payment.CBEBirr.Services;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Appdiv.Payment.CBEBirr;

public static class Startup
{
    public static IServiceCollection AddCBEBirr<TImplementation>(this IServiceCollection services)
        where TImplementation : class, ICBEBirrPayment
    {
        return AddCBEBirr(services)
            .AddScoped<ICBEBirrPayment, TImplementation>();
    }
    public static IServiceCollection AddCBEBirr(this IServiceCollection services)
    {
        return services.AddSoapCore()
            .AddScoped<ICBEService, CBEService>()
            .AddScoped<ICBESharedService, CBEService>();
    }

    public static IApplicationBuilder UseCBEBirr(
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
            SoapSerializer.XmlSerializer, true);
        builder.UseTelebirr<ICBESharedService, CBEMessage>(endpoint, paymentQueryPath: "private", paymentValidationPath,
            paymentConfirmationPath);
        return builder;
    }
}