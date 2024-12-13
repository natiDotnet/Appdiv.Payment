using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Requests;
[XmlType(Namespace = Namespace.C2B)]
public class KYCInfo
{
    [XmlElement(Namespace = Namespace.C2B)]
    public string KYCName { get; set; } = string.Empty;

    [XmlElement(Namespace = Namespace.C2B)]
    public string KYCValue { get; set; } = string.Empty;
}