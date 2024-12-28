using Appdiv.Payment.CBE;
using Appdiv.Payment.CBE.Requests;
using Appdiv.Payment.CBE.Responses;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.API;

public class CBEPayment : ICBEPayment
{
    public Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request)
    {
        return Task.FromResult(new ApplyTransactionResponse
        {
            BillRefNumber = "423323",
            Amount = 10.0m,
            CustomerName = "Sample Customer Name",
            TransID = "1wdcc",
            ShortCode = "123456"
        });
    }

    public Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)
    {
        return Task.FromResult(new C2BPaymentValidationResult());
    }
}