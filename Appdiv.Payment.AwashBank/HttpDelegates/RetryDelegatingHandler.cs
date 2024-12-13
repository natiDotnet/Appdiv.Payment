using Polly;
using Polly.Retry;

namespace Appdiv.Payment.AwashBank.HttpDelegates;

public sealed class RetryDelegatingHandler : DelegatingHandler
{
    private readonly RetryPolicy<HttpResponseMessage> _retryPolicy = Policy<HttpResponseMessage>.Handle<HttpRequestException>().RetryAsync(3);
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var policyResult = await _retryPolicy.ExecuteAndCaptureAsync(() => base.SendAsync(request, cancellationToken));
        if (policyResult.Outcome == OutcomeType.Failure)
        {
            throw new HttpRequestException("something went wrong", policyResult.FinalException);
        }
        return policyResult.Result;
    }
}