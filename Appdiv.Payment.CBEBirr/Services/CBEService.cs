﻿using System.Xml.Serialization;
using Appdiv.Payment.Shared.Exceptions;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.CBEBirr.Services;

// ReSharper disable once InconsistentNaming
internal class CBEService : ICBESharedService, ICBEService
{
    private readonly ICBEBirrPayment _payment;

    public CBEService(ICBEBirrPayment payment)
    {
        _payment = payment;
    }

    public async Task<ApplyTransactionResponse> C2BPaymentQueryRequest(Header Header, Body Body)
    {
        Body.BillReferenceNumber = Body.Parameters.Where(p => p.Key == nameof(Body.BillReferenceNumber))
                                            .Select(p => p.Value)
                                            .FirstOrDefault();
        Body.MSISDN = Body.Parameters.Where(p => p.Key == nameof(Body.MSISDN))
                                      .Select(p => p.Value)
                                      .FirstOrDefault();
        Body.ShortCode = Body.Parameters.Where(p => p.Key == nameof(Body.ShortCode))
                                         .Select(p => p.Value)
                                         .FirstOrDefault();
        var request = new ApplyTransactionRequest
        {
            Header = Header,
            Body = Body
        };
        var response = await _payment.PaymentQueryAsync(request);
        if (response.ResponseCode != 0)
        {
            response.Parameters = null;
            return response;
        }
        var parameters = new Parameter[5];
        parameters[0] = new Parameter
        {
            Key = nameof(response.BillRefNumber),
            Value = response.BillRefNumber ?? response.Parameters?
                    .FirstOrDefault(p => p.Key == nameof(response.BillRefNumber))?.Value
                ?? Body.BillReferenceNumber ?? throw new MissingParameterException(nameof(response.BillRefNumber))
        };
        parameters[1] = new Parameter
        {
            Key = nameof(response.TransID),
            Value = response.TransID ?? response.Parameters?
                    .FirstOrDefault(p => p.Key == nameof(response.TransID))?.Value
                ?? throw new MissingParameterException(nameof(response.TransID))
        };
        parameters[2] = new Parameter
        {
            Key = nameof(response.CustomerName),
            Value = response.CustomerName ?? response.Parameters?
                    .FirstOrDefault(p => p.Key == nameof(response.CustomerName))?.Value
                ?? throw new MissingParameterException(nameof(response.CustomerName))
        };
        parameters[3] = new Parameter
        {
            Key = nameof(response.Amount),
            Value = response.Amount?.ToString() ?? response.Parameters?
                    .FirstOrDefault(p => p.Key == nameof(response.Amount))?.Value
                ?? throw new MissingParameterException(nameof(response.Amount))
        };
        parameters[4] = new Parameter
        {
            Key = nameof(response.ShortCode),
            Value = response.ShortCode ?? response.Parameters?
                    .FirstOrDefault(p => p.Key == nameof(response.ShortCode))?.Value
                ?? Body.ShortCode ?? throw new MissingParameterException(nameof(response.ShortCode))
        };
        response.Parameters = parameters;
        return response;
    }

    public Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber, string TransType,
        string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN,
        [XmlArrayItem("KYCInfo", Namespace = "")] [XmlElement(Namespace = "")]
        KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentConfirmationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentConfirmationAsync(request);
    }

    public Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType,
        string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN,
        [XmlArrayItem("KYCInfo", Namespace = "")] [XmlElement(Namespace = "")]
        KYCInfo[] KYCInfo)
    {
        var request = new C2BPaymentValidationRequest(BillRefNumber, TransType, TransID, TransTime, TransAmount,
            BusinessShortCode, MSISDN, KYCInfo);
        return _payment.PaymentValidationAsync(request);
    }
}