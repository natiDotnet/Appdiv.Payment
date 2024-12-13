namespace Appdiv.Payment.AwashBank.Contracts;

public class TransactionDetail
{
    public string TransactionStatus { get; set; } = string.Empty;
    public string TransactionReference { get; set; } = string.Empty;
    public decimal TransactionAmount { get; set; }
    public string DateProcessed { get; set; } = string.Empty;
    public string TransactionDetails { get; set; } = string.Empty;
}
