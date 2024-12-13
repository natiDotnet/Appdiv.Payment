namespace Appdiv.Payment.AwashBank.Contracts;

public class ApiResponse
{
    public string PaymentCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string OrderBy { get; set; } = string.Empty;
}
