using System.Text.Json;
using Appdiv.Payment.CBEBirr;
using Appdiv.Payment.Shared.Models;
using DirectPay.Application.Abstration;
using DirectPay.Domain.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DirectPay.Cbebirr.Payments;

public static class CbeApi
{
    public static void Endpoint(this IEndpointRouteBuilder route, CbeOptions options)
    {
        var group = route.MapGroup("/api")
            .WithTags("CBE Payments");
        group.MapPost("/CBE/CallbackPath", async ([FromBody] CbeOptions cbe, ISettingRepository settingRepository) =>
        {
            var setting = new Setting
            {
                Key = "CbeCallback",
                Configuration = JsonSerializer.Serialize(cbe)
            };
            await settingRepository.AddAsync(setting);

            // Restart the API host service to apply the new settings
            await File.WriteAllTextAsync("Plugins/restart.dll", "restart");
            return Results.Ok("Path updated successfully");
        });

        string paymentQuery = CombinePath(options.BasePath, options.PaymentQueryPath);
        group.MapPost(paymentQuery, ([FromBody] ApplyTransactionRequest request, ICBEBirrPayment cbePayment) =>
        {
            return cbePayment.PaymentQueryAsync(request);
        });

        string paymentValidation = CombinePath(options.BasePath, options.PaymentValidationPath);
        group.MapPost(paymentValidation, ([FromBody] C2BPaymentValidationRequest request, ICBEBirrPayment cbePayment) =>
        {
            return cbePayment.PaymentValidationAsync(request);
        });

        string paymentConfirmation = CombinePath(options.BasePath, options.PaymentConfirmationPath);
        group.MapPost(paymentConfirmation, ([FromBody] C2BPaymentConfirmationRequest request, ICBEBirrPayment cbePayment) =>
        {
            return cbePayment.PaymentConfirmationAsync(request);
        });

    }
    public static string CombinePath(params string[] paths)
        => string.Join("/", paths.Where(p => !string.IsNullOrEmpty(p))
                             .Select(p => p.Trim('/')));
}
