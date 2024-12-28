using System.Xml;
using Appdiv.Payment.Shared.Helper;
using Appdiv.Payment.Shared.Models;
using SoapCore;

namespace Appdiv.Payment.CBE.Services;

// ReSharper disable once InconsistentNaming
public class CBEMessage : CustomMessage
{
    private const string EnvelopeShortName = "SOAP-ENV";

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", Shared.Helper.Namespace.Soap1Envelope);
        writer.WriteXmlnsAttribute(EnvelopeShortName, Shared.Helper.Namespace.Soap1Envelope);
        writer.WriteXmlnsAttribute("ns1", Shared.Helper.Namespace.C2B);
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", Shared.Helper.Namespace.Soap1Envelope);
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