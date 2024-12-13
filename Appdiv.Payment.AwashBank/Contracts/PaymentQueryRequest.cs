namespace Appdiv.Payment.AwashBank.Contracts;

public class PaymentQueryRequest
{
    public string PaymentCode { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
}
