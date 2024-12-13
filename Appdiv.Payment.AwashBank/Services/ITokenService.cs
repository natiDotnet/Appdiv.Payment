namespace Appdiv.Payment.AwashBank.Services;

public interface ITokenService
{
    void SetToken(string token, TimeSpan expiration);
    string GetToken();
    bool IsTokenExpired();
}
