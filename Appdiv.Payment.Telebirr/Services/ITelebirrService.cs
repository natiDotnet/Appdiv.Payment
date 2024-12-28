using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Shared.Helper;
using Appdiv.Payment.Shared.Models;

// ReSharper disable InconsistentNaming

namespace Appdiv.Payment.Telebirr.Services;

[ServiceContract(Namespace = Namespace.C2B)]
internal interface ITelebirrService : ISharedService
{
    [OperationContract]
    Task<C2BPaymentQueryResult> C2BPaymentQueryRequest(string TransType, string TransID, string TransTime,
        string BusinessShortCode, string BillRefNumber, string MSISDN);

    [OperationContract(Action = "ValidateC2BPayment")]
    new Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(
        string BillRefNumber,
        string TransType,
        string TransID,
        string TransTime,
        decimal TransAmount,
        string BusinessShortCode,
        string MSISDN,
        [XmlElement(Namespace = "")] [XmlArrayItem(nameof(KYCInfo), Namespace = "")]
        KYCInfo[] KYCInfo);

    [OperationContract(Action = "ConfirmC2BPayment")]
    new Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(
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