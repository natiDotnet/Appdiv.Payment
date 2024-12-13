using Appdiv.C2BPayment.CBEbirr.Responses;
using Appdiv.C2BPayment.CBEbirr.Requests;
using Appdiv.Payment.Telebirr.Requests;

namespace Appdiv.C2BPayment.CBEbirr;
public interface ICBEbirrPayment
{
    Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request);
    Task<C2BPaymentValidationResult> PaymentValidation(C2BPaymentValidationRequest request);
    Task<C2BPaymentConfirmationResult> PaymentComfirmation(C2BPaymentConfirmationRequest request);
}
