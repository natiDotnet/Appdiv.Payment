using System.ComponentModel.DataAnnotations;
using System.ServiceModel;
using System.Xml.Serialization;
using Appdiv.Payment.CBEBirr.Requests;

namespace Appdiv.Payment.CBEBirr.Responses;

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

    [XmlElement(Namespace = Namespace.AT)] public int ResponseCode { get; init; }

    [XmlElement(Namespace = Namespace.AT)] public string ResponseDesc { get; set; } = string.Empty;

    [XmlArray(nameof(Parameters), Namespace = Namespace.AT)]
    [XmlArrayItem(nameof(Parameter), Namespace = Namespace.GOA)]
    public Parameter[]? Parameters { get; set; } = null;

    [XmlIgnore] public string? BillRefNumber { get; set; }

    [XmlIgnore] public string? CustomerName { get; set; }

    [XmlIgnore] public decimal? Amount { get; set; }

    [XmlIgnore] public string? TransID { get; set; }
    [XmlIgnore] public string? ShortCode { get; set; }
}