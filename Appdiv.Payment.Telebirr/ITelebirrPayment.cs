using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.Telebirr;

public interface ITelebirrPayment
{
    Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request);
    Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request);
    Task<C2BPaymentConfirmationResult> PaymentConfirmation(C2BPaymentConfirmationRequest request);
}
