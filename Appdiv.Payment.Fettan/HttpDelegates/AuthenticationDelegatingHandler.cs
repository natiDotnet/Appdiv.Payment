using Appdiv.Payment.Fettan.Requests;
using Microsoft.Extensions.Options;

namespace Appdiv.Payment.Fettan.HttpDelegates;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly FettanOptions _options;

    public AuthenticationDelegatingHandler(IOptions<FettanOptions> options)
    {
        _options = options.Value;
    }
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // request.DefaultRequestHeaders.Add("HDR_Signature", owner.Signature);
        // request.DefaultRequestHeaders.Add("HDR_IPAddress", owner.IPAddress);
        // request.DefaultRequestHeaders.Add("HDR_UserName", owner.UserName);
        // request.DefaultRequestHeaders.Add("HDR_Password", owner.Password);
        return base.SendAsync(request, cancellationToken);
    }
}