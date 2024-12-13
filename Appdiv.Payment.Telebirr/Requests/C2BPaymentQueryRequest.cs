using System.Xml.Serialization;

namespace Appdiv.Payment.Telebirr.Requests;
[XmlType]
public class C2BPaymentQueryRequest
{
    [XmlNamespaceDeclarations] private readonly XmlSerializerNamespaces _namespaceSpace = new();


    public C2BPaymentQueryRequest()
    {
        _namespaceSpace.Add("c2b", Namespace.C2B);

    }
    //[XmlElement]
    public string BillRefNumber { get; set; } = string.Empty;
    //[XmlElement]
    public string TransType { get; set; } = string.Empty;
    //[XmlElement]
    // ReSharper disable once InconsistentNaming
    public string TransID { get; set; } = string.Empty;
    //[XmlElement]
    public string TransTime { get; set; } = string.Empty;
    //[XmlElement]
    public string BusinessShortCode { get; set; } = string.Empty;
    //[XmlElement]
    public string MSISDN { get; set; } = string.Empty;


}
