using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.Telebirr.Requests;
using Appdiv.Payment.Telebirr.Responses;

namespace Appdiv.Payment.Telebirr.Services;
[ServiceContract(Namespace = Namespace.C2B)]
public interface ITelebirrService
{
    [OperationContract]
    Task<C2BPaymentQueryResult> C2BPaymentQueryRequest(string TransType, string TransID, string TransTime, string BusinessShortCode, string BillRefNumber, string MSISDN);

    [OperationContract(Action = "ValidateC2BPayment")]
    Task<C2BPaymentValidationResult> C2BPaymentValidationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, [XmlElement("KYCInfo")] KYCInfo[] KYCInfo);

    [OperationContract(Action = "ConfirmC2BPayment")]
    Task<C2BPaymentConfirmationResult> C2BPaymentConfirmationRequest(string BillRefNumber, string TransType, string TransID, string TransTime, decimal TransAmount, string BusinessShortCode, string MSISDN, KYCInfo[] KYCInfo);

}
