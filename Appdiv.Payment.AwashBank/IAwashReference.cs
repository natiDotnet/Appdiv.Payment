using Appdiv.Payment.AwashBank.Contracts;

namespace Appdiv.Payment.AwashBank;
/// <summary>
/// Interface for handling Awash Bank payment operations
/// </summary>
public interface IAwashReference
{
    /// <summary>
    /// Authenticates user credentials with Awash Bank API
    /// </summary>
    /// <param name="credential">User credentials for authentication</param>
    /// <returns>Authentication response containing token and status</returns>
    Task<AuthResponse> AuthenticateUserAsync(Credential credential);

    /// <summary>
    /// Fetches payment query details from Awash Bank
    /// </summary>
    /// <param name="request">Payment query request parameters</param>
    /// <returns>API response containing payment query results</returns>
    Task<ApiResponse> PaymentQueryAsync(PaymentQueryRequest request);

    /// <summary>
    /// Confirms a payment transaction with Awash Bank
    /// </summary>
    /// <param name="request">Payment confirmation request details</param>
    /// <returns>API response containing payment confirmation status</returns>
    Task<ApiResponse> PaymentConfirmAsync(PaymentConfirmationRequest request);
}
