using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Appdiv.Payment.TelebirrClient.Models;
using Appdiv.Payment.TelebirrClient.Contracts;

namespace Appdiv.Payment.TelebirrClient;

public class TelebirrClient : ITelebirrClient
{
    public const string TokenPath = "/payment/v1/token/";
    public const string AuthTokenPath = "/payment/v1/auth/authToken";
    public const string PreOrderPath = "/payment/v1/merchant/preOrder";
    private readonly HttpClient client;
    private readonly TelebirrConfig config;

    public TelebirrClient(HttpClient client, IOptions<TelebirrConfig> config)
    {
        this.client = client;
        this.config = config.Value;
    }
    public async Task<FabricTokenResponse> ApplyFabricToken()
    {
        var request = new FabricTokenRequest
        {
            AppSecret = config.AppSecret
        };
        var bodyString = JsonSerializer.Serialize(request, Helper.SerializeOptions);
        var response = await client.PostAsJsonAsync(TokenPath, bodyString);
        return await response.Content.ReadFromJsonAsync<FabricTokenResponse>();
    }

    public async Task<string> AuthToken()
    {
        var request = new AuthTokenRequest
        {
            Method = "payment.authtoken",
            Version = "1.0",
            BizContent = new TokenBiz
            {
                AccessToken = config.AppSecret,
                TradeType = "InApp",
                AppId = config.MerchantAppId,
                ResourceType = "OpenId",
            }
        };
        request.Sign = Helper.Sign(request, config.PrivateKey);
        var body = JsonSerializer.Serialize(request, Helper.SerializeOptions);

        var response = await client.PostAsJsonAsync(AuthTokenPath, body);

        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> CreateOrder(string title, decimal amount)
    {
        var request = new Order();
        request.BizContent = new OrderBiz
        {
            Appid = config.MerchantAppId,
            MerchCode = config.ShortCode,
            MerchOrderId = Helper.GetCurrentTimestamp().ToString(),
            TradeType = "InApp",
            Title = title,
            TotalAmount = amount,
            TransCurrency = "ETB",
            TimeoutExpress = "120m",
            PayeeIdentifier = "220311",
            PayeeIdentifierType = "04",
            PayeeType = "5000"
        };
        request.Sign = Helper.Sign(request, config.PrivateKey);
        var body = JsonSerializer.Serialize(request, Helper.SerializeOptions);
        var response = await client.PostAsJsonAsync(PreOrderPath, body);
        var responseBody = await response.Content.ReadFromJsonAsync<dynamic>();
        var prepayId = responseBody.BizContent.PrepayId;
        var orderResponse = new OrderResponse
        {

        };
        orderResponse.Sign = Helper.Sign(orderResponse, config.PrivateKey);
        return Helper.Sign(orderResponse);
    }

    Task<FabricTokenResponse> ITelebirrClient.ApplyFabricToken()
    {
        throw new NotImplementedException();
    }
}
