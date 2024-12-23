using System.Xml;
using SoapCore;
//using SoapCore;

//using System.ServiceModel.Channels;

namespace Appdiv.Payment.Telebirr.Services;

public class TelebirrCustomMessage : CustomMessage
{
    private const string EnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
    private const string EnvelopeShortName = "soapenv";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", EnvelopeNamespace);
        writer.WriteStartElement(EnvelopeShortName, "Header", EnvelopeNamespace);
        writer.WriteEndElement();
    }
    
    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", EnvelopeNamespace);
    }

}
