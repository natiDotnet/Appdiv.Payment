using System.Xml;
using Appdiv.Payment.Shared.Helper;
using SoapCore;

//using System.ServiceModel.Channels;

namespace Appdiv.Payment.Telebirr.Services;

public class TelebirrMessage : CustomMessage
{
    private const string EnvelopeShortName = "soapenv";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", Namespace.Soap1Envelope);
        writer.WriteStartElement(EnvelopeShortName, "Header", Namespace.Soap1Envelope);
        writer.WriteEndElement();
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", Namespace.Soap1Envelope);
    }
}