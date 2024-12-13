using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Requests;
[XmlType]
public class C2BPaymentConfirmationRequest
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
    [XmlArray]
    public KYCInfo[] KYCInfos { get; set; } = Array.Empty<KYCInfo>();
}
