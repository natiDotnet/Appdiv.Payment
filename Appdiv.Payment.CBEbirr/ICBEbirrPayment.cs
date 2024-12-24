using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;

namespace Appdiv.Payment.CBEbirr;

public interface ICBEbirrPayment
{
    Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request);
}