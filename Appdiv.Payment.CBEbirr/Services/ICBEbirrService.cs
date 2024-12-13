using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;
using Appdiv.C2BPayment.CBEbirr.Requests;
using System.Threading.Tasks;
using Appdiv.C2BPayment.CBEbirr.Responses;
using System.ServiceModel;
using System.Xml.Serialization;

namespace Appdiv.C2BPayment.CBEbirr.Services;
[ServiceContract(Namespace = Namespace.Tem)]
public interface ICBEbirrService
{
    [OperationContract(Name = "ApplyTransactionRequest")]
    Task<ApplyTransactionResponse> C2BPaymentQueryRequest([XmlElement(Namespace = Namespace.AT)] Header Header, [XmlElement(Namespace = Namespace.AT)] Body Body);
}