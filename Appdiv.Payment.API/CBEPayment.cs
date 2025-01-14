using Appdiv.Payment.CBEBirr;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.API;

public class CBEBirrPayment : ICBEBirrPayment
{
    private readonly HttpClient _httpClient;

    public CBEBirrPayment(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApplyTransactionResponse> PaymentQueryAsync(ApplyTransactionRequest request)
    {
        _httpClient.GetAsync("");
        return Task.FromResult(new ApplyTransactionResponse
        {
            BillRefNumber = "423323",
            Amount = 10.0m,
            CustomerName = "Sample Customer Name",
            TransID = "1wdcc",
            ShortCode = "123456"
        });
    }

    public Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        return Task.FromResult(new C2BPaymentValidationResult());
    }
}