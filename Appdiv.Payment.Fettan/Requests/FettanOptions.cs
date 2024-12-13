using System.Text.Json.Serialization;

namespace Appdiv.Payment.Fettan.Requests;

public class FettanOptions
{
    public const string Fettan = "Fettan";
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("Signature")]
    public string Signature { get; set; } = string.Empty;
    [JsonPropertyName("IPAddress")]
    public string IPAddress { get; set; } = string.Empty;
    [JsonPropertyName("UserName")]
    public string UserName { get; set; } = string.Empty;
    [JsonPropertyName("Password")]
    public string Password { get; set; } = string.Empty;
    [JsonPropertyName("MerchantID")]
    public string MerchantID { get; set; } = string.Empty;
}
