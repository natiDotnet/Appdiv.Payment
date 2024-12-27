﻿using System.Xml.Serialization;
using Appdiv.Payment.CBEbirr.Exceptions;
using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;
using Appdiv.Payment.Telebirr.Services;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBEbirrService : ICBESharedService, ICBEbirrService
{
    private readonly ICBEbirrPayment _payment;

    public CBEbirrService(ICBEbirrPayment payment)
    {
        _payment = payment;
    }

    public Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, [XmlArrayItem("KYCInfo", Namespace = ""), XmlElement(Namespace = "")] KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentConfirmationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount, BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentConfirmation(request);
    }

    public async Task<ApplyTransactionResponse> C2BPaymentQueryRequest(Header Header, Body Body)
    {
        var request = new ApplyTransactionRequest
        {
            Header = Header,
            Body = Body
        };
        var response = await _payment.PaymentQuery(request);
        var parameters = new Parameter[5];
        parameters[0] = new()
        {
            Key = nameof(response.BillRefNumber),
            Value = response.BillRefNumber ?? response.Parameters
                    .FirstOrDefault(p => p.Key == nameof(response.BillRefNumber))?.Value
                ?? throw new MissingParameterException(nameof(response.BillRefNumber))
        };
        parameters[1] = new()
        {
            Key = nameof(response.TransID),
            Value = response.TransID ?? response.Parameters
                    .FirstOrDefault(p => p.Key == nameof(response.TransID))?.Value
                ?? throw new MissingParameterException(nameof(response.TransID))
        };
        parameters[2] = new()
        {
            Key = nameof(response.CustomerName),
            Value = response.CustomerName ?? response.Parameters
                        .FirstOrDefault(p => p.Key == nameof(response.CustomerName))?.Value
                    ?? throw new MissingParameterException(nameof(response.CustomerName))
        };
        parameters[3] = new()
        {
            Key = nameof(response.Amount),
            Value = response.Amount?.ToString() ?? response.Parameters
                    .FirstOrDefault(p => p.Key == nameof(response.Amount))?.Value
                ?? throw new MissingParameterException(nameof(response.Amount))
        };
        parameters[4] = new()
        {
            Key = nameof(response.ShortCode),
            Value = response.ShortCode ?? response.Parameters
                    .FirstOrDefault(p => p.Key == nameof(response.ShortCode))?.Value
                ?? throw new MissingParameterException(nameof(response.ShortCode))
        };
        response.Parameters = parameters;
        return response;
    }

    public Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, [XmlArrayItem("KYCInfo", Namespace = ""), XmlElement(Namespace = "")] KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentValidationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount, BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentValidation(request);
    }
}