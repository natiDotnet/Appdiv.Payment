using System.Net.Http.Headers;
using Appdiv.Payment.AwashBank.Contracts;
using Appdiv.Payment.AwashBank.HttpDelegates;
using Appdiv.Payment.AwashBank.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Appdiv.Payment.AwashBank;

public static class Startup
{
    public static IServiceCollection AddAwashClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddScoped<IAwashClient, AwashClient>()
            .AddSingleton<ITokenService, TokenService>()
            .Configure<AwashConfig>(configuration.GetSection(AwashConfig.Key))
            .AddHttpClient<AwashClient>(async (serviceProvider, client) =>
            {
                var config = serviceProvider.GetRequiredService<IOptions<AwashConfig>>().Value;
                client.BaseAddress = new Uri(config.Url);
            })
            .AddHttpMessageHandler<RetryDelegatingHandler>()
            .AddHttpMessageHandler<AuthenticationDelegatingHandler>();
        return services;

    }
    public static void UseAwashEndpoint(this IApplicationBuilder builder, string endpoint = "/Awash")
    {
        //builder.map.MapGet("/hsll", c => c.Response.WriteAsync());
        //builder.UseSoapEndpoint<ITelebirrService, TelebirrCustomMessage>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer);

    }


}
