namespace Appdiv.Payment.Telebirr.Requests;

public class C2BPaymentValidationRequest
{
    public string BillRefNumber { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string TransID { get; set; } = string.Empty;
    public string TransTime { get; set; } = string.Empty;
    public decimal TransAmount { get; set; } = decimal.Zero;
    public string BusinessShortCode { get; set; } = string.Empty;
    public string MSISDN { get; set; } = string.Empty;
    public KYCInfo[] KYCInfos { get; set; } = Array.Empty<KYCInfo>();
}