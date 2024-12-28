using Appdiv.Payment.Telebirr.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

namespace Appdiv.Payment.Telebirr;

public static class Startup
{
    public static IServiceCollection AddTelebirr<TImplementation>(this IServiceCollection services)
        where TImplementation : class, ITelebirrPayment
    {
        return services.AddSoapCore()
            .AddSingleton<ITelebirrService, TelebirrService>()
            .AddSingleton<ITelebirrPayment, TImplementation>();
    }

    public static IApplicationBuilder UseTelebirr(this IApplicationBuilder builder, string endpoint = "/Telebirr")
    {
        return builder.UseTelebirr<ITelebirrService, TelebirrCustomMessage>(endpoint);
    }

    public static IApplicationBuilder UseTelebirr<S, T>(this IApplicationBuilder builder, string endpoint = "/Telebirr",
        string paymentQueryPath = "/paymentQuery", string paymentValidationPath = "/paymentValidation",
        string paymentConfirmationPath = "/paymentConfirmation") where T : CustomMessage, new()
    {
        builder.UseSoapEndpoint<S, T>($"{endpoint}{paymentQueryPath}", new SoapEncoderOptions(),
            SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<S, T>($"{endpoint}{paymentValidationPath}", new SoapEncoderOptions(),
            SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<S, T>($"{endpoint}{paymentConfirmationPath}", new SoapEncoderOptions(),
            SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<S, T>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer, true);
        return builder;
    }
}