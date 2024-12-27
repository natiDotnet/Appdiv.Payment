using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;
using Appdiv.Payment.Telebirr.Shared;

namespace Appdiv.Payment.Telebirr;

/// <summary>
/// Interface for handling Telebirr payment operations.
/// </summary>
public interface ITelebirrPayment : ISharedPayment
{
    /// <summary>
    /// Queries the status of a C2B payment.
    /// </summary>
    /// <param name="request">The request object containing the details of the payment query.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the payment query result.</returns>
    Task<C2BPaymentQueryResult> PaymentQuery(C2BPaymentQueryRequest request);
}
