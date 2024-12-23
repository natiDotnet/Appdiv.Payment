using System.ServiceModel;
using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Responses;
[MessageContract(WrapperName = nameof(C2BPaymentConfirmationResult), IsWrapped = true, WrapperNamespace = Namespace.C2B)]
public class C2BPaymentConfirmationResult
{
    [XmlText]
    public int ResultCode { get; set; }
}
