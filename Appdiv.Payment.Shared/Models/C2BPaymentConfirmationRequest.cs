namespace Appdiv.Payment.Shared.Models;

public class C2BPaymentConfirmationRequest
{
    public C2BPaymentConfirmationRequest(string billRefNumber, string transType, string transId, string transTime,
        decimal transAmount, string businessShortCode, string msisdn, KYCInfo[] kycInfos)
    {
        BillRefNumber = billRefNumber;
        TransType = transType;
        TransID = transId;
        TransTime = transTime;
        TransAmount = transAmount;
        BusinessShortCode = businessShortCode;
        MSISDN = msisdn;
        FirstName = kycInfos[0].KYCValue;
        MiddleName = kycInfos[1].KYCValue;
        LastName = kycInfos[2].KYCValue;
    }

    public string BillRefNumber { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string TransID { get; set; } = string.Empty;
    public string TransTime { get; set; } = string.Empty;
    public decimal TransAmount { get; set; } = decimal.Zero;
    public string BusinessShortCode { get; set; } = string.Empty;
    public string MSISDN { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}