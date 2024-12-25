using System.Xml;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Telebirr.Helper;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBETransactionCustomMessage : CustomMessage
{
    private const string EnvelopeNamespace = "http://www.w3.org/2003/05/soap-envelope";
    private const string EnvelopeShortName = "env";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", EnvelopeNamespace);
        writer.WriteXmlnsAttribute(EnvelopeShortName, EnvelopeNamespace);
        writer.WriteXmlnsAttribute("ns1", Namespace.AT);
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", EnvelopeNamespace);
    }

    protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("tem", nameof(ApplyTransactionResponse), Namespace.Tem);
        writer.WriteXmlnsAttribute("at", Namespace.AT);
        writer.WriteXmlnsAttribute("goa", Namespace.GOA);
        writer.WriteXmlnsAttribute("tem", Namespace.Tem);
        using var bodyReader = Message.GetReaderAtBodyContents();
        XmlHelper.WriteXmlNode(bodyReader, writer, true);
    }

}
