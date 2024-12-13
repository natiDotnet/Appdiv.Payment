using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Responses;
[XmlType]
public class C2BPaymentValidationResult
{
    //[XmlElement]
    public int ResultCode { get; set; }
    //[XmlElement]
    public string ResultDesc { get; set; } = string.Empty;
    //[XmlElement]
    public string ThirdPartyTransID { get; set; } = string.Empty;

}
