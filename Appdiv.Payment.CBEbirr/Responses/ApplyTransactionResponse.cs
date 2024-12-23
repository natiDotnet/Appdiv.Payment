using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.CBEbirr.Requests;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Responses;
[XmlRoot(Namespace = Namespace.Tem)]
[MessageContract(IsWrapped = true, WrapperName = nameof(ApplyTransactionResponse))]
public class ApplyTransactionResponse
{
    [XmlNamespaceDeclarations] public XmlSerializerNamespaces Namespaces = new();
    public ApplyTransactionResponse()
    {
        Namespaces.Add("at", Namespace.AT);
        Namespaces.Add("goa", Namespace.GOA);
        Namespaces.Add("tem", Namespace.Tem);
    }
    [XmlElement(Namespace = Namespace.AT)]
    public int ResponseCode { get; set; }
    [XmlElement(Namespace = Namespace.AT)]
    public string ResponseDesc { get; set; } = string.Empty;

    [XmlArray(nameof(Parameters), Namespace = Namespace.AT)]
    [XmlArrayItem(nameof(Parameter), Namespace = Namespace.GOA)]
    public Parameter[] Parameters { get; set; } = Array.Empty<Parameter>();
}
