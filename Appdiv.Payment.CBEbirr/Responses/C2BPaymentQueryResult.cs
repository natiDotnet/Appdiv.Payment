using System;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Appdiv.C2BPayment.CBEbirr.Responses;
[XmlType]
public class C2BPaymentQueryResult
{
    [XmlElement]
    public string ResultCode { get; set; }
    [XmlElement]
    public string ResultDesc { get; set; }
    [XmlElement]
    public string TransID { get; set; }
    [XmlElement]
    public string BillRefNumber { get; set; }
    [XmlElement]
    public string UtilityName { get; set; }
    [XmlElement]
    public string CustomerName { get; set; }
    [XmlElement]
    public decimal Amount { get; set; }
}