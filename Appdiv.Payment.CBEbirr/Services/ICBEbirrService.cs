using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.CBEbirr.Services;

[ServiceContract(Namespace = Namespace.Tem)]
public interface ICBEbirrService
{
    [OperationContract(Name = "ApplyTransactionRequest")]
    Task<ApplyTransactionResponse> C2BPaymentQueryRequest([XmlElement(Namespace = Namespace.AT)] Header Header,
        [XmlElement(Namespace = Namespace.AT)] Body Body);
}

[ServiceContract(Namespace = Shared.Helper.Namespace.C2B)]
public interface ICBESharedService : ISharedService
{
    [OperationContract(Action = "C2BPaymentValidation")]
    new Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(
        string BillRefNumber,
        string TransType,
        string TransID,
        string TransTime,
        decimal TransAmount,
        string BusinessShortCode,
        string MSISDN,
        [XmlElement] [XmlArrayItem(nameof(KYCInfo), Namespace = "")]
        KYCInfo[] KYCInfo);

    [OperationContract(Action = "C2BPaymentConfirmation")]
    new Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(
        string BillRefNumber,
        string TransType,
        string TransID,
        string TransTime,
        decimal TransAmount,
        string BusinessShortCode,
        string MSISDN,
        [XmlElement] [XmlArrayItem(nameof(KYCInfo), Namespace = "")]
        KYCInfo[] KYCInfo);
}