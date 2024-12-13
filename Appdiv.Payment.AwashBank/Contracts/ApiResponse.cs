namespace Appdiv.Payment.AwashBank.Contracts;

public class ApiResponse
{
    public string PaymentCode { get; set; }
    public decimal Amount { get; set; }
    public bool Status { get; set; }
    public string Message { get; set; }
    public string CustomerName { get; set; }
    public string Reason { get; set; }
    public string AccountNumber { get; set; }
    public string OrderBy { get; set; }
}
