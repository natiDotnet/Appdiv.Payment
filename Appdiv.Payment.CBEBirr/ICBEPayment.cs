using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.CBEBirr;

/// <summary>
///     Interface for handling CBE payment operations.
/// </summary>
public interface ICBEBirrPayment : ISharedPayment
{
    /// <summary>
    ///     Queries the amount of a C2B payment.
    /// </summary>
    /// <param name="request">The request object containing the details of the payment query.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the payment query result.</returns>
    Task<ApplyTransactionResponse> PaymentQueryAsync(ApplyTransactionRequest request);
}