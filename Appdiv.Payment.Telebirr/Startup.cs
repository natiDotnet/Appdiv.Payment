using Appdiv.Payment.Telebirr.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SoapCore;

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

    public static IApplicationBuilder UseTelebirr(this IApplicationBuilder builder, string endpoint = "/Telebirr")
    {
        return builder.UseTelebirr<TelebirrCustomMessage>(endpoint);
    }

    public static IApplicationBuilder UseTelebirr<T>(this IApplicationBuilder builder, string endpoint = "/Telebirr",
        string paymentQueryPath = "/paymentQuery", string paymentValidationPath = "/paymentValidation", string paymentConfirmationPath = "/paymentConfirmation") where T : CustomMessage, new()
    {
        builder.UseSoapEndpoint<ITelebirrService, T>($"{endpoint}{paymentQueryPath}", new SoapEncoderOptions(), SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<ITelebirrService, T>($"{endpoint}{paymentValidationPath}", new SoapEncoderOptions(), SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<ITelebirrService, T>($"{endpoint}{paymentConfirmationPath}", new SoapEncoderOptions(), SoapSerializer.XmlSerializer, true);
        builder.UseSoapEndpoint<ITelebirrService, T>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer, true);
        return builder;
    }
}