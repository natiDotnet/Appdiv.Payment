using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.CBEbirr;
public interface ICBEbirrPayment
{
    Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request);
    Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request);
    Task<C2BPaymentConfirmationResult> PaymentComfirmation(C2BPaymentConfirmationRequest request);
}
