namespace Appdiv.Payment.TelebirrClient.Models;

public class TelebirrConfig
{
    public string BaseUrl { get; set; }
    public string MerchantAppId { get; set; }
    public string FabricAppId { get; set; }
    public string ShortCode { get; set; }
    public string AppSecret { get; set; }
    public string PrivateKey { get; set; }
}
