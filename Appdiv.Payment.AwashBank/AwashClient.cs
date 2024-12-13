using System.Net.Http.Json;
using Appdiv.Payment.AwashBank.Contracts;
using Appdiv.Payment.AwashBank.Services;

namespace Appdiv.Payment.AwashBank;

public class AwashClient : IAwashClient
{
    private readonly HttpClient _httpClient;
    private readonly ITokenService _tokenService;

    public AwashClient(HttpClient httpClient, ITokenService tokenService)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
    }

    public async Task<AccountInfo> AccountEnquireAsync(string account, string bankCode = "awsbnk")
    {
        return await GetFromJsonAsync<AccountInfo>($"/monetize/getAccount?bankCode={bankCode}&account={account}");
    }

    public async Task<TransactionDetail> ApproveOtpAsync(OtpInfo otp)
    {
        return await PostAsJsonAsync<OtpInfo, TransactionDetail>("/monetize/validate", otp);
    }

    public Task<TransactionDetail> ApproveOtpAsync(string phone, string otp)
    {
        if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone cannot be null or empty", nameof(phone));
        if (string.IsNullOrWhiteSpace(otp)) throw new ArgumentException("OTP cannot be null or empty", nameof(otp));

        return ApproveOtpAsync(new OtpInfo { Phone = phone, Otp = otp });
    }

    public async Task<Credential> ChangeCredentialAsync(Credential credential)
    {
        return await PostAsJsonAsync<Credential, Credential>("/apps/change", credential);
    }

    public async Task<TokenInfo> GetTokenAsync(Credential credential)
    {
        var tokenInfo = await PostAsJsonAsync<Credential, TokenInfo>("/auth/getToken", credential);
        _tokenService.SetToken(tokenInfo.Token, TimeSpan.FromHours(24));
        return tokenInfo;
    }

    public async Task<TransactionDetail> GetTransactionAsync(string reference)
    {
        if (string.IsNullOrWhiteSpace(reference)) throw new ArgumentException("Reference cannot be null or empty", nameof(reference));

        return await GetFromJsonAsync<TransactionDetail>($"/monetize/getTransaction?reference={reference}");
    }

    public async Task<string> PingAsync()
    {
        return await _httpClient.GetStringAsync("/ping");
    }

    public async Task<TransactionDetail> TransferFundAsync(Transaction transaction)
    {
        return await PostAsJsonAsync<Transaction, TransactionDetail>("/monetize/post", transaction);
    }

    #region Private Helper Methods

    private async Task<TResponse> GetFromJsonAsync<TResponse>(string uri)
    {
        var response = await _httpClient.GetAsync(uri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>() 
               ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    private async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string uri, TRequest content)
    {
        var response = await _httpClient.PostAsJsonAsync(uri, content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>() 
               ?? throw new InvalidOperationException("Failed to deserialize response.");
    }

    #endregion
}
