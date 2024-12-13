namespace Appdiv.Payment.AwashBank.Contracts;

public class TransactionDetail
{
    public string TransactionStatus { get; set; }
    public string TransactionReference { get; set; }
    public decimal TransactionAmount { get; set; }
    public string DateProcessed { get; set; }
    public string TransactionDetails { get; set; }
}
