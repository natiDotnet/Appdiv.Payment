using System;
using System.Linq;
using System.Collections.Generic;
using SoapCore;
using System.Xml;
using System.ServiceModel.Channels;

namespace Appdiv.C2BPayment.CBEbirr.Services;

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
