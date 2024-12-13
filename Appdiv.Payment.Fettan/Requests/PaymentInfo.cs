namespace Appdiv.Payment.Fettan.Requests;

public class PaymentInfo
{
    public string CardNumber { get; set; } = string.Empty;
    public string ExpirationDate { get; set; } = string.Empty;
    public string PIN { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public string SourceTransID { get; set; } = string.Empty;
}