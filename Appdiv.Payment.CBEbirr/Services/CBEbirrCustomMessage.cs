using System.Xml;
using Appdiv.Payment.Shared.Helper;
using Appdiv.Payment.Shared.Models;
using SoapCore;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBEbirrCustomMessage : CustomMessage
{
    private const string EnvelopeNamespace = "http://www.w3.org/2003/05/soap-envelope";
    private const string EnvelopeShortName = "SOAP-ENV";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", EnvelopeNamespace);
        writer.WriteXmlnsAttribute(EnvelopeShortName, EnvelopeNamespace);
        writer.WriteXmlnsAttribute("ns1", Shared.Helper.Namespace.C2B);
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", EnvelopeNamespace);
    }

    protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
    {
        var apis = new[]
            { nameof(C2BPaymentConfirmationResult), nameof(C2BPaymentValidationResult), nameof(C2BPaymentQueryResult) };
        var api = apis.FirstOrDefault(key => Message.ToString().Contains(key));
        if (api is not null) writer.WriteStartElement("c2bpayment", api, Shared.Helper.Namespace.C2B);
        using var bodyReader = Message.GetReaderAtBodyContents();
        XmlHelper.WriteXmlNode(bodyReader, writer, false);
    }
}