using Appdiv.Payment.CBEbirr;
using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.API;

public class CBEbirrPayment : ICBEbirrPayment
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