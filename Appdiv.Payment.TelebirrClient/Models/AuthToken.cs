using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Appdiv.Payment.TelebirrClient.Contracts;

namespace Appdiv.Payment.TelebirrClient.Models;

public class RequestBody
{
    public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();
    //[JsonPropertyName("nonce_str")]
    public string NonceStr { get; set; } = Helper.RandomText();
    public virtual string Method { get; set; }
    public string Version { get; set; } = "1.0";
    public string Sign { get; set; }
    public string SignType { get; set; } = "SHA256WithRSA";

}
public class AuthTokenRequest : RequestBody
{
    public override string Method { get; set; } = "payment.authtoken";
    public TokenBiz BizContent { get; set; }
}

public class TokenBiz
{
    public string AccessToken { get; set; }
    public string TradeType { get; set; }
    [JsonPropertyName("appid")]
    public string AppId { get; set; }
    public string ResourceType { get; set; }
}