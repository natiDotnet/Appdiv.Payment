using System.Xml.Serialization;
using Appdiv.Payment.Shared.Models;

// ReSharper disable InconsistentNaming

namespace Appdiv.Payment.Shared.Contracts;

public interface ISharedService
{
    Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(
        string BillRefNumber,
        string TransType,
        string TransID,
        string TransTime,
        decimal TransAmount,
        string BusinessShortCode,
        string MSISDN,
        [XmlElement(Namespace = "")] [XmlArrayItem(nameof(KYCInfo), Namespace = "")]
        KYCInfo[] KYCInfo);

    Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(
        string BillRefNumber,
        string TransType,
        string TransID,
        string TransTime,
        decimal TransAmount,
        string BusinessShortCode,
        string MSISDN,
        [XmlElement(Namespace = "")] [XmlArrayItem(nameof(KYCInfo), Namespace = "")]
        KYCInfo[] KYCInfo);
}