using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;

namespace Appdiv.Payment.CBEbirr.Services;
[ServiceContract(Namespace = Namespace.Tem)]
public interface ICBEbirrService
{
    [OperationContract(Name = "ApplyTransactionRequest")]
    Task<ApplyTransactionResponse> C2BPaymentQueryRequest([XmlElement(Namespace = Namespace.AT)] Header Header, [XmlElement(Namespace = Namespace.AT)] Body Body);
}