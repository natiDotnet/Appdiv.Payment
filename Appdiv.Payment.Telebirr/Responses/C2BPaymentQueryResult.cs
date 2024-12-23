using System.ServiceModel;
using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Responses;
// [XmlType]
[MessageContract(IsWrapped = true, WrapperName = nameof(C2BPaymentQueryResult), WrapperNamespace = Namespace.C2B)]
public class C2BPaymentQueryResult
{
    [XmlElement]
    public string ResultCode { get; set; } = string.Empty;
    [XmlElement]
    public string ResultDesc { get; set; } = string.Empty;
    [XmlElement]
    public string TransID { get; set; } = string.Empty;
    [XmlElement]
    public string BillRefNumber { get; set; } = string.Empty;
    [XmlElement]
    public string UtilityName { get; set; } = string.Empty;
    [XmlElement]
    public string CustomerName { get; set; } = string.Empty;
    [XmlElement]
    public decimal Amount { get; set; }
}