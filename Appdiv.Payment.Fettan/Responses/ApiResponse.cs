using System.Text.Json.Serialization;

namespace Appdiv.Payment.Fettan.Responses;

public class ApiResponse
{
    [JsonPropertyName("HDR_ResponseID")]
    public long ResponseID { get; set; }

    [JsonPropertyName("HDR_TimeStamp")]
    public DateTime TimeStamp { get; set; }

    [JsonPropertyName("HDR_TimeStamp")]
    public string Aknowledge { get; set; } = string.Empty;

    [JsonPropertyName("HDR_ReferenceNumber")]
    public string ReferenceNumber { get; set; } = string.Empty;

    [JsonPropertyName("HDR_SourceTransID")]
    public string SourceTransID { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AutherizationCode")]
    public string AutherizationCode { get; set; } = string.Empty;

    [JsonPropertyName("BODY_PaymentCardType")]
    public string PaymentCardType { get; set; } = string.Empty;

    [JsonPropertyName("BODY_CardNumber")]
    public string CardNumber { get; set; } = string.Empty;

    [JsonPropertyName("BODY_Amount")]
    public decimal Amount { get; set; }

    [JsonPropertyName("BODY_PaymentAction")]
    public string PaymentAction { get; set; } = string.Empty;

    [JsonPropertyName("MSG_ErrorCode")]
    public string ErrorCode { get; set; } = string.Empty;

    [JsonPropertyName("MSG_ShortMessage")]
    public string ShortMessage { get; set; } = string.Empty;

    [JsonPropertyName("MSG_LongMessage")]
    public string LongMessage { get; set; } = string.Empty;

}
