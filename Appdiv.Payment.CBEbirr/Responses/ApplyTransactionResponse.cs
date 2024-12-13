using System;
using System.Linq;
using System.Collections.Generic;
using Appdiv.C2BPayment.CBEbirr.Requests;
using System.Xml.Serialization;

namespace Appdiv.C2BPayment.CBEbirr.Responses;
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

    public int ResponseDesc { get; set; }

    [XmlArray(nameof(Parameters))]
    [XmlArrayItem(nameof(Parameter), Namespace = Namespace.GOA)]
    public Parameter[] Parameters { get; set; }
}
