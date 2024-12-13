using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Appdiv.C2BPayment.CBEbirr.Responses;
[XmlType]
public class C2BPaymentConfirmationResult
{
    [XmlText]
    public int ResultCode { get; set; }
}
