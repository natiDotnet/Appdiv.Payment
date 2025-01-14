using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;

namespace Appdiv.Payment.API;

public class TelebirrPayment : ITelebirrPayment
{
    private readonly HttpClient _httpClient;
    public TelebirrPayment(HttpClient httpClient)
    {
        _httpClient = httpClient;

    }
    public Task<C2BPaymentQueryResult> PaymentQueryAsync(C2BPaymentQueryRequest request)
    {
        Console.WriteLine(_httpClient.BaseAddress?.ToString());
        _httpClient.GetAsync("");
        return Task.FromResult(new C2BPaymentQueryResult());
    }

    public Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        return Task.FromResult(new C2BPaymentValidationResult());
    }

    public Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        return Task.FromResult(new C2BPaymentConfirmationResult());
    }
}