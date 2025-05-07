using System.Text.Json;
using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Abstration;
using DirectPay.Domain.Settings;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.Telebirr.Payment;

public static class TelebirrApi
{
    public static void Endpoints(this IEndpointRouteBuilder routes, TelebirrOptions options)
    {
        var group = routes.MapGroup("/api")
            .WithTags("Telebirr Payments");
        group.MapPost("/Telebirr/CallbackPath", async ([FromBody] TelebirrOptions telebirr, ISettingRepository settingRepository) =>
        {
            var setting = new Setting
            {
                Key = "TelebirrCallback",
                Configuration = JsonSerializer.Serialize(telebirr)
            };
            await settingRepository.AddAsync(setting);

            // Restart the API host service to apply the new settings
            await File.WriteAllTextAsync("Plugins/restart.dll", "restart");
            Results.Ok("Path updated successfully");
        });

        string paymentQuery = CombinePath(options.BasePath, options.PaymentQueryPath);
        group.MapPost(paymentQuery, ([FromBody] C2BPaymentQueryRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentQueryAsync(request);
        });

        string paymentValidation = CombinePath(options.BasePath, options.PaymentValidationPath);
        group.MapPost(paymentValidation, ([FromBody] C2BPaymentValidationRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentValidationAsync(request);
        });

        string paymentConfirmation = CombinePath(options.BasePath, options.PaymentConfirmationPath);
        group.MapPost(paymentConfirmation, ([FromBody] C2BPaymentConfirmationRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentConfirmationAsync(request);
        });
    }

    public static string CombinePath(params string[] paths)
        => string.Join("/", paths.Where(p => !string.IsNullOrEmpty(p))
                             .Select(p => p.Trim('/')));
}
