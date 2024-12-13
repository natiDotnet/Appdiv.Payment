using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.Telebirr;

public class TelebirrPayment : ITelebirrPayment
{
    public Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request)
    {
        throw new System.NotImplementedException();
    }

    public Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)
    {
        throw new System.NotImplementedException();
    }

    public Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)
    {
        throw new System.NotImplementedException();
    }
}