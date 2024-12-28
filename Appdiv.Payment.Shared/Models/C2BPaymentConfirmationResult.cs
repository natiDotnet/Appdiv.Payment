using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.Shared.Helper;

namespace Appdiv.Payment.Shared.Models;

[MessageContract(WrapperName = nameof(C2BPaymentConfirmationResult), IsWrapped = true,
    WrapperNamespace = Namespace.C2B)]
public class C2BPaymentConfirmationResult
{
    [XmlText] public int ResultCode { get; set; }
}