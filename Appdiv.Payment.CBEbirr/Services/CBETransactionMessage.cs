using System.Xml;
using Appdiv.Payment.CBEbirr.Responses;
using Appdiv.Payment.Shared.Helper;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBETransactionMessage : CustomMessage
{
    private const string EnvelopeShortName = "env";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", Shared.Helper.Namespace.Soap12Envelope);
        writer.WriteXmlnsAttribute(EnvelopeShortName, Shared.Helper.Namespace.Soap12Envelope);
        writer.WriteXmlnsAttribute("ns1", Namespace.AT);
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", Shared.Helper.Namespace.Soap12Envelope);
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