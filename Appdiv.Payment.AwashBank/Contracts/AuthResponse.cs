namespace Appdiv.Payment.AwashBank.Contracts;

public class AuthResponse
{
    public bool Status { get; set; }
    public string Token { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
