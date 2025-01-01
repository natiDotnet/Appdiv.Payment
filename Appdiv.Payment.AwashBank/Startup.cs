using Appdiv.Payment.AwashBank.Contracts;
using Appdiv.Payment.AwashBank.HttpDelegates;
using Appdiv.Payment.AwashBank.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appdiv.Payment.AwashBank;

public static class Startup
{
    public static IServiceCollection AddAwashClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IAwashClient, AwashClient>()
            .AddSingleton<ITokenService, TokenService>()
            .Configure<AwashConfig>(configuration.GetSection(AwashConfig.Key))
            .AddHttpClient<AwashClient>((serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<AwashConfig>>().Value;
                client.BaseAddress = new Uri(config.Url);
            })
            .AddHttpMessageHandler<RetryDelegatingHandler>()
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
        return services;

    }
    public static void UseAwashEndpoint(this IEndpointRouteBuilder endpoints, string endpoint = "/Awash", string authPath = "/Authentication", string paymentQueryPath = "/PaymentQuery", string paymentConfirmationPath = "/PaymentConfirmation")
    {
        endpoints.MapPost($"{endpoint}{authPath}", async ([FromBody] Credential request, [FromServices] IAwashReference awash) =>
        {
            var response = await awash.AuthenticateUserAsync(request);
            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        });
        endpoints.MapPost($"{endpoint}{paymentQueryPath}", async ([FromBody] PaymentQueryRequest request, [FromServices] IAwashReference awash) =>
        {
            var response = await awash.PaymentQueryAsync(request);
            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        });
        endpoints.MapPost($"{endpoint}{paymentConfirmationPath}", async ([FromBody] PaymentConfirmationRequest request, [FromServices] IAwashReference awash) =>
        {
            var response = await awash.PaymentConfirmAsync(request);
            return response.Status ? Results.Ok(response) : Results.BadRequest(response);
        });
    }


}
