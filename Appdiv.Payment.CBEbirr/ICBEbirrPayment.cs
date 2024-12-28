using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Shared.Contracts;

namespace Appdiv.Payment.CBEbirr;

public interface ICBEbirrPayment : ISharedPayment
{
    Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request);
}