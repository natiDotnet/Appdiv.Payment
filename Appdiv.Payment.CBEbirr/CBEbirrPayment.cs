using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.CBEbirr;

public class CBEbirrPayment : ICBEbirrPayment
{
    public Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request)
    {
        throw new System.NotImplementedException();
    }
    

    public Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)
    {
        throw new System.NotImplementedException();
    }

    public Task<C2BPaymentConfirmationResult> PaymentComfirmation(C2BPaymentConfirmationRequest request)
    {
        throw new System.NotImplementedException();
    }
}