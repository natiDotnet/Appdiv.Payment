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
    /// <param name="additionalInformation">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> SaleAsync(PaymentInfo paymentInfo, params string[] additionalInformation);

    /// <summary>
    /// Processes a deposit transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInformation">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> DepositAsync(PaymentInfo paymentInfo, params string[] additionalInformation);

    /// <summary>
    /// Processes a refund transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInformation">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> RefundAsync(PaymentInfo paymentInfo, params string[] additionalInformation);

    /// <summary>
    /// Processes a withdrawal transaction.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="additionalInformation">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> WithdrawalAsync(PaymentInfo paymentInfo, params string[] additionalInformation);

    /// <summary>
    /// Purchases airtime for the specified vendor.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendor">The vendor for airtime (default is EthioTelecom).</param>
    /// <param name="additionalInformation">Optional additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> AirtimeAsync(PaymentInfo paymentInfo, string vendor = AirtimeVendor.EthioTelecom, params string[] additionalInformation);

    /// <summary>
    /// Looks up bill information for the specified vendor account.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendorAccount">The vendor account associated with the bill.</param>
    /// <param name="additionalInformation">additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> BillLookupAsync(PaymentInfo paymentInfo, string vendorAccount, params string[] additionalInformation);

    /// <summary>
    /// Processes a bill payment for the specified vendor account.
    /// </summary>
    /// <param name="paymentInfo">The payment information.</param>
    /// <param name="vendorAccount">The vendor account to which the bill payment is made.</param>
    /// <param name="additionalInformation">additional information.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the API response.</returns>
    Task<ApiResponse> BillPaymentAsync(PaymentInfo paymentInfo, string vendorAccount, params string[] additionalInformation);
}