using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.Telebirr.Services;

public class TelebirrService : ITelebirrService
{
    private readonly ITelebirrPayment _payment;

    public TelebirrService(ITelebirrPayment payment)
    {
        _payment = payment;
    }
    public async Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, KYCInfo[] KYCInfos)
    {
        var request = new C2BPaymentConfirmationRequest
        {
            BillRefNumber = BillRefNumber,
            TransType = TransType,
            TransID = TransID,
            TransTime = TransTime,
            TransAmount = TransAmount,
            BusinessShortCode = BusinessShortCode,
            MSISDN = MSISDN,
            KYCInfos = KYCInfos
        };
        return await _payment.PaymentConfirmation(request);
    }

    public Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(C2BPaymentValidationRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<C2BPaymentQueryResult> C2BPaymentQueryRequest(string TransType, string TransID, string TransTime, string BusinessShortCode, string BillRefNumber, string MSISDN)
    {
        var request = new C2BPaymentQueryRequest
        {
            TransType = TransType,
            TransID = TransID,
            TransTime = TransTime,
            BusinessShortCode = BusinessShortCode,
            BillRefNumber = BillRefNumber,
            MSISDN = MSISDN
        };
        return await _payment.PaymentQuery(request);
    }

    public async Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentValidationRequest
        {
            BillRefNumber = BillRefNumber,
            TransType = TransType,
            TransID = TransID,
            TransTime = TransTime,
            TransAmount = TransAmount,
            BusinessShortCode = BusinessShortCode,
            MSISDN = MSISDN,
            KYCInfos = KYCInfo
        };
        return await _payment.PaymentValidation(request);
    }
}
