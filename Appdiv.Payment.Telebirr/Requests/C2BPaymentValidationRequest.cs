using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Requests;
[XmlType("C2BPaymentValidationRequest", Namespace = Namespace.C2B)]
// [XmlRoot("C2BPaymentValidationRequest", Namespace = Namespace.C2B)]
public class C2BPaymentValidationRequest
{
    [XmlElement]
    public string BillRefNumber { get; set; } = string.Empty;
    [XmlElement]
    public string TransType { get; set; } = string.Empty;
    [XmlElement]
    public string TransID { get; set; } = string.Empty;
    [XmlElement]
    public string TransTime { get; set; } = string.Empty;
    [XmlElement]
    public decimal TransAmount { get; set; } = decimal.Zero;
    [XmlElement]
    public string BusinessShortCode { get; set; } = string.Empty;
    [XmlElement] 
    public string MSISDN { get; set; } = string.Empty;
    [XmlElement("KYCInfo")]
    public KYCInfo[] KYCInfos { get; set; } = Array.Empty<KYCInfo>();
}
