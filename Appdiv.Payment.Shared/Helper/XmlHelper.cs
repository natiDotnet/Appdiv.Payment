using System.Xml;

namespace Appdiv.Payment.Shared.Helper;

public static class XmlHelper
{
    public static void WriteXmlNode(XmlDictionaryReader reader, XmlWriter writer, bool inheritNamespace)
    {
        while (reader.Read())
            if (reader.NodeType == XmlNodeType.Element)
            {
                WriteStartElement(reader, writer, inheritNamespace);

                if (!reader.IsEmptyElement)
                {
                    reader.Read();
                    WriteElementContent(reader, writer, inheritNamespace);
                }

                writer.WriteEndElement();
            }
    }

    private static void WriteStartElement(XmlDictionaryReader reader, XmlWriter writer, bool inheritNamespace)
    {
        if (inheritNamespace)
            writer.WriteStartElement(reader.LocalName, reader.NamespaceURI);
        else
            writer.WriteStartElement(reader.LocalName);
        writer.WriteAttributes(reader, false);
    }

    private static void WriteElementContent(XmlDictionaryReader reader, XmlWriter writer, bool inheritNamespace)
    {
        while (reader.NodeType != XmlNodeType.EndElement)
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element:
                    WriteStartElement(reader, writer, inheritNamespace);
                    if (!reader.IsEmptyElement)
                    {
                        reader.Read();
                        WriteElementContent(reader, writer, inheritNamespace);
                    }

                    writer.WriteEndElement();
                    break;

                case XmlNodeType.Text:
                    writer.WriteString(reader.Value);
                    break;

                default:
                    writer.WriteNode(reader, false);
                    break;
            }

            reader.Read();
        }
    }
}