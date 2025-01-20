namespace Appdiv.Payment.TelebirrClient.Models;

public class Order : RequestBody
{
    public override string Method { get; set; } = "payment.preorder";
    public OrderBiz BizContent { get; set; }
}
public class OrderBiz
{
    public string NotifyUrl { get; set; }
    public string TradeType { get; set; }
    public string Appid { get; set; }
    public string MerchCode { get; set; }
    public string MerchOrderId { get; set; }
    public string Title { get; set; }
    public decimal TotalAmount { get; set; }
    public string TransCurrency { get; set; }
    public string TimeoutExpress { get; set; }
    public string BusinessType { get; set; }
    public string PayeeIdentifier { get; set; }
    public string PayeeIdentifierType { get; set; }
    public string PayeeType { get; set; }
}

public class OrderResponse
{
    public string AppId { get; set; }
    public string MerchCode { get; set; }
    public string NonceStr { get; set; }
    public string PrepayId { get; set; }
    public string Timestamp { get; set; }
    public string SignType { get; set; }
    public string Sign { get; set; }
}
