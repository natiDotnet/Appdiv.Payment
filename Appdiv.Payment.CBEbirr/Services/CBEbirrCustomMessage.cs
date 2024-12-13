using System.Xml;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Services;

public class CBEbirrCustomMessage : CustomMessage
{
    private const string EnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private const string EnvelopeShortName = "env";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", EnvelopeNamespace);
        writer.WriteStartElement(EnvelopeShortName, "Header", EnvelopeNamespace);
        writer.WriteStartElement(EnvelopeShortName, "Body", EnvelopeNamespace);
        writer.WriteEndElement();
    }

}
