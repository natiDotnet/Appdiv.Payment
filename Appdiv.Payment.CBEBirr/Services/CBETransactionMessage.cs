﻿using System.ServiceModel.Channels;
using System.Xml;
using Appdiv.Payment.Shared.Helper;
using Appdiv.Payment.Shared.Models;
using SoapCore;

namespace Appdiv.Payment.CBEBirr.Services;

// ReSharper disable once InconsistentNaming
public class CBETransactionMessage : CustomMessage
{
    private const string EnvelopeShortName = "env";

    private bool HasError => !Message.ToString().Contains("<at:ResponseCode>0</at:ResponseCode>");

    protected override void OnWriteStartEnvelope(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement(EnvelopeShortName, "Envelope", Shared.Helper.Namespace.Soap12Envelope);
        writer.WriteXmlnsAttribute(EnvelopeShortName, Shared.Helper.Namespace.Soap12Envelope);
        if (HasError)
        {
            writer.WriteXmlnsAttribute("ns1", Namespace.AT);
            writer.WriteXmlnsAttribute("ns2", Namespace.GOA);
            writer.WriteXmlnsAttribute("ns3", Namespace.Tem);
        }
        else
        {
            writer.WriteXmlnsAttribute("ns1", Namespace.AT);
        }
    }

    protected override void OnWriteStartBody(XmlDictionaryWriter writer)
    {
        writer.WriteStartElement("Body", Shared.Helper.Namespace.Soap12Envelope);
    }

    protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
    {
        if (HasError)
        {
            writer.WriteStartElement(nameof(ApplyTransactionResponse), Namespace.Tem);
        }
        else
        {
            writer.WriteStartElement("tem", nameof(ApplyTransactionResponse), Namespace.Tem);
            writer.WriteXmlnsAttribute("at", Namespace.AT);
            writer.WriteXmlnsAttribute("goa", Namespace.GOA);
            writer.WriteXmlnsAttribute("tem", Namespace.Tem);
        }
        using var bodyReader = Message.GetReaderAtBodyContents();
        XmlHelper.WriteXmlNode(bodyReader, writer, true);
    }
}