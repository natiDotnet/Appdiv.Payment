using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.CBE.Requests;
using Appdiv.Payment.CBE.Responses;
using Appdiv.Payment.Shared.Contracts;
using Appdiv.Payment.Shared.Models;

namespace Appdiv.Payment.CBE.Services;

[ServiceContract(Namespace = Namespace.Tem)]
public interface ICBEService
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