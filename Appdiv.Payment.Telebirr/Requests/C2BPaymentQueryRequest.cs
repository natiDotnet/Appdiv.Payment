namespace Appdiv.Payment.Telebirr.Requests;

public class C2BPaymentQueryRequest
{
    public string BillRefNumber { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string TransID { get; set; } = string.Empty;
    public string TransTime { get; set; } = string.Empty;
    public string BusinessShortCode { get; set; } = string.Empty;
    public string MSISDN { get; set; } = string.Empty;

    public C2BPaymentQueryRequest(string billRefNumber, string transType, string transId, string transTime, string businessShortCode, string msisdn)
    {
        BillRefNumber = billRefNumber;
        TransType = transType;
        TransID = transId;
        TransTime = transTime;
        BusinessShortCode = businessShortCode;
        MSISDN = msisdn;
    }
}