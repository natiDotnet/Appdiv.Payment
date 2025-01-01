namespace Appdiv.Payment.AwashBank.Services;

public class TokenService : ITokenService
{
    private string _token = string.Empty;
    private DateTime _expirationDate = DateTime.UtcNow;

    public string GetToken()
    {
        return _token;
    }

    public bool IsTokenExpired()
    {
        return DateTime.UtcNow >= _expirationDate || string.IsNullOrWhiteSpace(_token);
    }

    public void SetToken(string token, TimeSpan expiration)
    {
        _token = token;
        _expirationDate = DateTime.UtcNow.Add(expiration);
    }
}
