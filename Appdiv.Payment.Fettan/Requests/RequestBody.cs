using System.Text.Json.Serialization;

namespace Appdiv.Payment.Fettan.Requests;

//[DisplayName("21molePay")]
public class RequestBody
{
    [JsonPropertyName("BODY_CardNumber")]
    public string CardNumber { get; set; } = string.Empty;

    [JsonPropertyName("BODY_ExpirationDate")]
    public string ExpirationDate { get; set; } = string.Empty;

    [JsonPropertyName("BODY_PIN")]
    public string PIN { get; set; } = string.Empty;

    [JsonPropertyName("BODY_PaymentAction")]
    public string PaymentAction { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AmountX")]
    public decimal Amount { get; set; } = decimal.Zero;

    [JsonPropertyName("BODY_MerchantID")]
    public string MerchantID { get; set; } = string.Empty;

    [JsonPropertyName("BODY_OrderDescription")]
    public string OrderDescription { get; set; } = string.Empty;

    [JsonPropertyName("BODY_SourceTransID")]
    public string SourceTransID { get; set; } = string.Empty;

    [JsonPropertyName("BODY_VendorAccount")]
    public string VendorAccount { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AdditionalInfo1")]
    public string AdditionalInfo1 { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AdditionalInfo2")]
    public string AdditionalInfo2 { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AdditionalInfo3")]
    public string AdditionalInfo3 { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AdditionalInfo4")]
    public string AdditionalInfo4 { get; set; } = string.Empty;

    [JsonPropertyName("BODY_AdditionalInfo5")]
    public string AdditionalInfo5 { get; set; } = string.Empty;

    public static implicit operator RequestBody(PaymentInfo payment)
    {
        return new RequestBody
        {
            CardNumber = payment.CardNumber,
            PIN = payment.PIN,
            ExpirationDate = payment.ExpirationDate,
            OrderDescription = payment.Description,
            SourceTransID = payment.SourceTransID,
            Amount = payment.Amount
        };
    }

}