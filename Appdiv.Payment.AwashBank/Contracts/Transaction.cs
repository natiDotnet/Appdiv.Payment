namespace Appdiv.Payment.AwashBank.Contracts;

public class Transaction
{
    public string BankCode { get; set; } = "awsbnk";
    public decimal Amount { get; set; }
    public string Reference { get; set; }
    public string Narration { get; set; }
    public string AwashAccount { get; set; }
    public string CreditAccount { get; set; }
    public decimal CommisionAmount { get; set; }
    public string CommisionAccount { get; set; }
    public string AwashAccountName { get; set; }
    public string CreditAccountName { get; set; }
}
