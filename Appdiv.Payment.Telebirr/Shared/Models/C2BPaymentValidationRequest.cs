namespace Appdiv.Payment.Shared.Models;

public class C2BPaymentValidationRequest
{
    public C2BPaymentValidationRequest() { }
    public C2BPaymentValidationRequest(string billRefNumber, string transType, string transId, string transTime,
        decimal transAmount, string businessShortCode, string msisdn, KYCInfo[] kycInfos)
    {
        BillRefNumber = billRefNumber;
        TransType = transType;
        TransID = transId;
        TransTime = transTime;
        TransAmount = transAmount;
        BusinessShortCode = businessShortCode;
        MSISDN = msisdn;
        FirstName = kycInfos.Where(i => i.KYCName == nameof(FirstName))
                            .Select(i => i.KYCValue)
                            .FirstOrDefault() ?? string.Empty;
        MiddleName = kycInfos.Where(i => i.KYCName == nameof(MiddleName))
                            .Select(i => i.KYCValue)
                            .FirstOrDefault() ?? string.Empty;
        LastName = kycInfos.Where(i => i.KYCName == nameof(LastName))
                            .Select(i => i.KYCValue)
                            .FirstOrDefault() ?? string.Empty;
    }

    public string BillRefNumber { get; set; } = string.Empty;
    public string TransType { get; set; } = string.Empty;
    public string TransID { get; set; } = string.Empty;
    public string TransTime { get; set; } = string.Empty;
    public decimal TransAmount { get; set; }
    public string BusinessShortCode { get; set; } = string.Empty;
    public string MSISDN { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string MiddleName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}