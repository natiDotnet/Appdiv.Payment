namespace Appdiv.Payment.Fettan.Requests;

public class AirtimeVendor
{
    public const string EthioTelecom = "ETC";
    public const string Safaricom = "SAFARICOM";
}

public enum AirtimeVendors
{
    ETC,
    SAFARICOM
}

/// <summary>
/// Represents vendor codes for various payment services and merchants
/// </summary>
public enum VendorCode
{
    /// <summary>
    /// Digital Content Fees and Event Sales
    /// </summary>
    CONTENT,

    /// <summary>
    /// CANAL+ Satellite Subscription Service
    /// </summary>
    CANAL,

    /// <summary>
    /// Ethiopian Revenue Service tax payments
    /// </summary>
    DERASH,

    /// <summary>
    /// DStv Satellite TV/Internet Service
    /// </summary>
    DSTV,

    /// <summary>
    /// Emirates Airlines - PNR based inquiry and payment
    /// </summary>
    EMAIR,

    /// <summary>
    /// Ethiopian Airlines - PNR based inquiry and payment
    /// </summary>
    ETAIR,

    /// <summary>
    /// Government ETHSwitch online bills
    /// </summary>
    ETHSWITCH,

    /// <summary>
    /// Airlines reservation payments
    /// </summary>
    GUZOGO,

    /// <summary>
    /// Liyu Bus Tickets
    /// </summary>
    LIYUBUS,

    /// <summary>
    /// Qatar Airlines - PNR based inquiry and payment
    /// </summary>
    QATAR,

    /// <summary>
    /// Unicash School fees system
    /// </summary>
    UNICASH,

    /// <summary>
    /// WeBirr bill payment services
    /// </summary>
    WEBIRR,

    /// <summary>
    /// WebSprix ISP and other services
    /// </summary>
    WEBSPRIX
}

