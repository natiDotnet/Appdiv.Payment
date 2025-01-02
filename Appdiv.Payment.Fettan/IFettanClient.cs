using Appdiv.Payment.Fettan.Requests;
using Appdiv.Payment.Fettan.Responses;

namespace Appdiv.Payment.Fettan;
/// <summary>
/// Defines the contract for interacting with the Fettan payment system.
/// </summary>
public interface IFettanClient
{
    /// <summary>
    /// Authorizes a payment using the provided card number.
    /// </summary>
    /// <param name="cardNumber">The card number for the authorization.</param>
    /// <param name="sourceTransID">The optional source transaction ID.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> AuthorizationAsync(string cardNumber, string sourceTransID = "");

    /// <summary>
    /// Processes a sale transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInfo">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> SaleAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo2 = null);

    /// <summary>
    /// Processes a deposit transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInfo">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> DepositAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo5 = null);

    /// <summary>
    /// Processes a refund transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInfo">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> RefundAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null);

    /// <summary>
    /// Processes a withdrawal transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInfo">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> WithdrawalAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo5 = null);

    /// <summary>
    /// Processes an airtime top-up request.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendorAccount">The airtime vendor.</param>
    /// <param name="additionalInfo1">
    /// Ethio Telecom = P or B (P=PIN-based, purchase virtual card or B=Bulk, purchase pinless bulk minutes to be automatically transferred to recipient mobile.
    /// Safaricom = R (R=Recharge/Top-up a customer’s mobile balance)
    /// </param>
    /// <param name="additionalInfo2">Receiving customer mobile number if purchase is on behalf of another customer. If blank, customer making purchase will receive the airtime top-up.</param>
    /// <param name="additionalInfo3">Reserved for future use.</param>
    /// <param name="additionalInfo4">Reserved for future use.</param>
    /// <param name="additionalInfo5">Merchant ID/Merchant Mobile/Cash Till/Agent Device ID for transactions taken on behalf of another merchant.</param>
    /// <returns>The API response.</returns>
    /// <exception cref="ArgumentException">Thrown when additionalInfo1 is null or whitespace.</exception>
    Task<ApiResponse> AirtimeAsync(PaymentInfo paymentInfo, AirtimeVendors vendorAccount = AirtimeVendors.ETC, AirtimeTransactionType? additionalInfo1 = null, string? additionalInfo2 = null, string? additionalInfo5 = null);

    /// <summary>
    /// Looks up bill information for the specified vendor account.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendorAccount">The vendor account associated with the bill.</param>
    /// <param name="additionalInfo">additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> BillLookupAsync(PaymentInfo paymentInfo, VendorCode additionalInfo1, string additionalInfo2, string additionalInfo3);

    /// <summary>
    /// Processes a bill payment for the specified vendor account.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendorAccount">The vendor account to which the bill payment is made.</param>
    /// <param name="additionalInfo">additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> BillPaymentAsync(PaymentInfo paymentInfo, string vendorAccount, string additionalInfo1, string additionalInfo2, string additionalInfo3 = null, string? additionalInfo4 = null, string? additionalInfo5 = null);
}