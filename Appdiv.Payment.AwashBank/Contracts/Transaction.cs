namespace Appdiv.Payment.AwashBank.Contracts;

public class Transaction
{
    public string BankCode { get; set; } = "awsbnk";
    public decimal Amount { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Narration { get; set; } = string.Empty;
    public string AwashAccount { get; set; } = string.Empty;
    public string CreditAccount { get; set; } = string.Empty;
    public decimal CommissionAmount { get; set; } 
    public string CommissionAccount { get; set; } = string.Empty;
    public string AwashAccountName { get; set; } = string.Empty;
    public string CreditAccountName { get; set; } = string.Empty;
}
