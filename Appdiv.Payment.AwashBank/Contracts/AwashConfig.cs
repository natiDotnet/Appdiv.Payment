namespace Appdiv.Payment.AwashBank.Contracts;

public class AwashConfig
{
    public const string Key = "AwashBank";
    public string Url { get; set; } = "http://10.10.101.144/b2b/awash/api/v1";
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? ExpiryHours { get; set; } = 24;
}
