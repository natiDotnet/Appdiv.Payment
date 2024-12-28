using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.Telebirr.Services;

internal class TelebirrService : ITelebirrService
{
    private readonly ITelebirrPayment _payment;

    public TelebirrService(ITelebirrPayment payment)
    {
        _payment = payment;
    }

    public Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber,
        string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode,
        string MSISDN, KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentConfirmationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentConfirmationAsync(request);
    }

    public Task<C2BPaymentQueryResult> C2BPaymentQueryRequest(string TransType, string TransID, string TransTime,
        string BusinessShortCode, string BillRefNumber, string MSISDN)
    {
        var request =
            new C2BPaymentQueryRequest(BillRefNumber, TransType, TransID, TransTime, BusinessShortCode, MSISDN);
        return _payment.PaymentQueryAsync(request);
    }

    public Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType,
        string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN,
        KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentValidationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentValidationAsync(request);
    }
}