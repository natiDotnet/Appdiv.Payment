using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.Shared.Contracts;

public interface ISharedPayment
{
    /// <summary>
    ///     Validates a C2B payment.
    /// </summary>
    /// <param name="request">The request object containing the details of the payment validation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the payment validation result.</returns>
    Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request);

    /// <summary>
    ///     Confirms a C2B payment.
    /// </summary>
    /// <param name="request">The request object containing the details of the payment confirmation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the payment confirmation result.</returns>
    Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request);
}