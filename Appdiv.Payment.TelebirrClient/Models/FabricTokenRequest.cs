namespace Appdiv.Payment.TelebirrClient.Models;

public class FabricTokenRequest
{
    public string AppSecret { get; set; }
}
public class FabricTokenResponse
{
    public string Token { get; set; }
    public long EffectiveDate { get; set; }
    public long ExpirationDate { get; set; }
}
