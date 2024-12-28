using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;

namespace Appdiv.Payment.API;

public class TelebirrPayment : ITelebirrPayment
{
    public Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request)
    {
        return Task.FromResult(new C2BPaymentQueryResult());
    }

    public Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request)
    {
        return Task.FromResult(new C2BPaymentValidationResult());
    }

    public Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request)
    {
        return Task.FromResult(new C2BPaymentConfirmationResult());
    }
}