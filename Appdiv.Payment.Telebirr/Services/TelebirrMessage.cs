using System.Xml;
using Appdiv.Payment.Shared.Helper;
using Appdiv.Payment.Shared.Models;
using SoapCore;

namespace Appdiv.Payment.Telebirr.Services;

public class TelebirrMessage : CustomMessage
{
    private const string EnvelopeShortName = "soapenv";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", Namespace.Soap1Envelope);
        writer.WriteXmlnsAttribute(EnvelopeShortName, Namespace.Soap1Envelope);
        writer.WriteXmlnsAttribute("ns1", Namespace.C2B);
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", Namespace.Soap1Envelope);
    }

    protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
    {
        var apis = new[]
            { nameof(C2BPaymentConfirmationResult), nameof(C2BPaymentValidationResult), nameof(C2BPaymentQueryResult) };
        var api = apis.FirstOrDefault(key => Message.ToString().Contains(key));
        if (api is not null) writer.WriteStartElement("c2b", api, Shared.Helper.Namespace.C2B);
        using var bodyReader = Message.GetReaderAtBodyContents();
        XmlHelper.WriteXmlNode(bodyReader, writer, false);
    }
}