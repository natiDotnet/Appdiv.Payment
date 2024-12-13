using System.Xml.Serialization;
using Appdiv.Payment.CBEbirr.Requests;

namespace Appdiv.Payment.CBEbirr.Responses;
[XmlType(Namespace = Namespace.AT)]
public class ApplyTransactionResponse
{
    [XmlNamespaceDeclarations] private readonly XmlSerializerNamespaces _namespaces = new();


    public ApplyTransactionResponse()
    {
        _namespaces.Add("at", Namespace.AT);
        _namespaces.Add("goa", Namespace.GOA);
        _namespaces.Add("tem", Namespace.Tem);
    }

    public int ResponseCode { get; set; }

    public string ResponseDesc { get; set; } = string.Empty;

    [XmlArray(nameof(Parameters))]
    [XmlArrayItem(nameof(Parameter), Namespace = Namespace.GOA)]
    public Parameter[] Parameters { get; set; } = Array.Empty<Parameter>();
}
