using Appdiv.Payment.AwashBank.Contracts;

namespace Appdiv.Payment.AwashBank;

public interface IAwashReference
{
    Task<AuthResponse> Auth(Credential credential);
    Task<ApiResponse> PaymentQuery(PaymentQueryRequest request);
    Task<ApiResponse> PaymentConfirmation(PaymentComfirmationRequest request);
}
