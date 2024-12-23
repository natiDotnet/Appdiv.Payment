using System.Xml;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBEbirrCustomMessage : CustomMessage
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

}
