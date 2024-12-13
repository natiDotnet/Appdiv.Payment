using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Appdiv.C2BPayment.CBEbirr.Responses;
[XmlType]
public class C2BPaymentValidationResult
{
    [XmlElement]
    public int ResultCode { get; set; }
    [XmlElement]
    public string ResultDesc { get; set; }
    [XmlElement]
    public string ThirdPartyTransID { get; set; }

}
