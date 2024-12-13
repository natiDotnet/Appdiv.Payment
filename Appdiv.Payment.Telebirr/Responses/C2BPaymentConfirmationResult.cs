using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Responses;
[XmlType]
public class C2BPaymentConfirmationResult
{
    [XmlText]
    public int ResultCode { get; set; }
}
