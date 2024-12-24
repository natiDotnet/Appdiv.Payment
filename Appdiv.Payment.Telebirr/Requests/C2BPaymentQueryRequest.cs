namespace Appdiv.Payment.Telebirr.Requests;

public class C2BPaymentQueryRequest
{
    public string BillRefNumber { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string TransID { get; set; } = string.Empty;
    public string TransTime { get; set; } = string.Empty;
    public string BusinessShortCode { get; set; } = string.Empty;
    public string MSISDN { get; set; } = string.Empty;
}