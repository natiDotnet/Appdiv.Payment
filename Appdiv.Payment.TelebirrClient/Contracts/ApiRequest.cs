using System;

namespace Appdiv.Payment.TelebirrClient.Contracts;

public class ApiRequest
{
    public string AppId { get; set; } = string.Empty;

    public string Nonce { get; set; } = Guid.NewGuid().ToString("N");

    public string NotifyUrl { get; set; } = string.Empty;

    public string OutTradeNo { get; set; } = string.Empty;

    public string ReceiveName { get; set; } = string.Empty;

    public Dictionary<string, string>? ReturnApp { get; set; }

    public string ReturnUrl { get; set; } = string.Empty;

    public string ShortCode { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public int TimeoutExpress { get; set; } = 30;

    public long Timestamp { get; set; } = Helper.GetCurrentTimestamp();

    public decimal TotalAmount { get; set; }

}
