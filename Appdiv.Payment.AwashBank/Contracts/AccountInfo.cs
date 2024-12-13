namespace Appdiv.Payment.AwashBank.Contracts;

public class AccountInfo
{
    public bool Status { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
