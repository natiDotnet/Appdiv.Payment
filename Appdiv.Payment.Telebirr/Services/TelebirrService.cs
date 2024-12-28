using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.Telebirr.Services;

public class TelebirrService : ITelebirrService
{
    private readonly ITelebirrPayment _payment;

    public TelebirrService(ITelebirrPayment payment)
    {
        _payment = payment;
    }

    public async Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber,
        string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode,
        string MSISDN, KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentConfirmationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return await _payment.PaymentConfirmation(request);
    }

    public async Task<C2BPaymentQueryResult> C2BPaymentQueryRequest(string TransType, string TransID, string TransTime,
        string BusinessShortCode, string BillRefNumber, string MSISDN)
    {
        var request =
            new C2BPaymentQueryRequest(BillRefNumber, TransType, TransID, TransTime, BusinessShortCode, MSISDN);
        return await _payment.PaymentQuery(request);
    }

    public async Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType,
        string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN,
        KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentValidationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return await _payment.PaymentValidation(request);
    }
}