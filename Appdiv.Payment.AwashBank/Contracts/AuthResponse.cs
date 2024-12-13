namespace Appdiv.Payment.AwashBank.Contracts;

public class AuthResponse
{
    public bool Status { get; set; }
    public string Token { get; set; }
    public string Message { get; set; }
}
