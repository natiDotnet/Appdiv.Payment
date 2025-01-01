using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Authentication;
using Appdiv.Payment.AwashBank.Contracts;
using Appdiv.Payment.AwashBank.Services;
using Microsoft.Extensions.Options;

namespace Appdiv.Payment.AwashBank.HttpDelegates;

public sealed class AuthenticationDelegatingHandler : DelegatingHandler
{
    private const string Scheme = "Bearer";
    private readonly ITokenService _tokenService;
    private readonly AwashConfig _options;

    public AuthenticationDelegatingHandler(IOptions<AwashConfig> options, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _options = options.Value;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await SendWithToken(request, cancellationToken);
        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }
        _tokenService.SetToken(string.Empty, TimeSpan.Zero);
        return await SendWithToken(request, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendWithToken(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_tokenService.IsTokenExpired())
        {
            await GetNewToken(cancellationToken);
        }
        request.Headers.Authorization = new AuthenticationHeaderValue(Scheme, _tokenService.GetToken());
        return await base.SendAsync(request, cancellationToken);
    }
    private async Task GetNewToken(CancellationToken cancellationToken)
    {
        var credentials = new Credential(_options.UserName, _options.Password);
        var content = JsonContent.Create(credentials);
        var authRequest = new HttpRequestMessage(HttpMethod.Get, new Uri($"{_options.Url}/auth/getToken"))
        {
            Content = content
        };
        var response = await base.SendAsync(authRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
        var tokenInfo = await response.Content.ReadFromJsonAsync<TokenInfo>(cancellationToken: cancellationToken) ??
                        throw new AuthenticationException("Invalid username or password");
        _tokenService.SetToken(tokenInfo.Token, TimeSpan.FromHours(_options.ExpiryHours ?? 24));
    }
}