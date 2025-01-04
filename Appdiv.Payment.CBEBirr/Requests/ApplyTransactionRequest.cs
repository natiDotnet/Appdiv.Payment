using System.Xml.Serialization;

namespace Appdiv.Payment.CBEBirr.Requests;

public class ApplyTransactionRequest
{
    public Header Header { get; set; }
    public Body Body { get; set; }
}

[XmlType(Namespace = Namespace.GOA)]
public class Header
{
    public string CommandID { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string LoginID { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Timestamp { get; set; } = string.Empty;
    public string ConversationID { get; set; } = string.Empty;
}

[XmlType(Namespace = Namespace.AT)]
public class Body
{
    [XmlArray(nameof(Parameters), Namespace = Namespace.AT)]
    [XmlArrayItem(nameof(Parameter), Namespace = Namespace.GOA)]
    public Parameter[] Parameters { get; set; } = Array.Empty<Parameter>();
}

[XmlType(Namespace = Namespace.GOA)]
public class Parameter
{
    [XmlElement(Namespace = Namespace.GOA)]
    public string Key { get; set; } = string.Empty;

    [XmlElement(Namespace = Namespace.GOA)]
    public string Value { get; set; } = string.Empty;
}