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
        return AddTelebirr(services)
            .AddScoped<ITelebirrPayment, TImplementation>();
    }
    public static IServiceCollection AddTelebirr(this IServiceCollection services)
    {
        return services.AddSoapCore()
            .AddScoped<ITelebirrService, TelebirrService>();
    }

    public static IApplicationBuilder UseTelebirr(
        this IApplicationBuilder builder,
        string endpoint = "/Telebirr",
        string paymentQueryPath = "/paymentQuery",
        string paymentValidationPath = "/paymentValidation",
        string paymentConfirmationPath = "/paymentConfirmation")
    {
        return builder.UseTelebirr<ITelebirrService, TelebirrMessage>(endpoint, paymentQueryPath, paymentValidationPath, paymentConfirmationPath);
    }

    public static IApplicationBuilder UseTelebirr<S, T>(
        this IApplicationBuilder builder,
        string endpoint = "/Telebirr",
        string paymentQueryPath = "/paymentQuery",
        string paymentValidationPath = "/paymentValidation",
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