using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.Telebirr.Payment;

public static class TelebirrApi
{
    public static void Endpoints(this IEndpointRouteBuilder routes, TelebirrOptions options)
    {
        string paymentQuery = $"{options.BasePath}{options.PaymentQueryPath}";
        routes.MapPost(paymentQuery, ([FromBody] C2BPaymentQueryRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentQueryAsync(request);
        });

        string paymentValidation = $"{options.BasePath}{options.PaymentValidationPath}";
        routes.MapPost(paymentValidation, ([FromBody] C2BPaymentValidationRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentValidationAsync(request);
        });

        string paymentConfirmation = $"{options.BasePath}{options.PaymentConfirmationPath}";
        routes.MapPost(paymentConfirmation, ([FromBody] C2BPaymentConfirmationRequest request, ITelebirrPayment telebirrPayment) =>
        {
            return telebirrPayment.PaymentConfirmationAsync(request);
        });
    }
}
