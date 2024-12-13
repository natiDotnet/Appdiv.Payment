namespace Appdiv.Payment.AwashBank.Contracts;

public class PaymentQueryRequest
{
    public string PaymentCode { get; set; }
    public string PaymentMethod { get; set; }
}
