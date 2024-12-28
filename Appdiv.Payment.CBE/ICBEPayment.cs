using Appdiv.Payment.CBE.Requests;
using Appdiv.Payment.CBE.Responses;
using Appdiv.Payment.Shared.Contracts;

namespace Appdiv.Payment.CBE;

public interface ICBEPayment : ISharedPayment
{
    Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request);
}