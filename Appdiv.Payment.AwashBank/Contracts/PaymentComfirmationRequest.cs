namespace Appdiv.Payment.AwashBank.Contracts;

public class PaymentComfirmationRequest
{
    public string PaymentCode { get; set; }
    public decimal Amount { get; set; }
    public string TransId { get; set; }
}
