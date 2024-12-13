using Appdiv.Payment.Fettan.HttpDelegates;
using Appdiv.Payment.Fettan.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Appdiv.Payment.Fettan;

public static class Startup
{
    public static IServiceCollection AddFettanClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFettanClient, FettanClient>();
        services.AddTransient<RetryDelegatingHandler>();
        services.Configure<FettanOptions>(configuration.GetSection(FettanOptions.Fettan));
        services.AddHttpClient<FettanClient>((serviceProvider, client) =>
        {
            var owner = serviceProvider.GetRequiredService<IOptionsMonitor<FettanOptions>>().CurrentValue;
            client.BaseAddress = new Uri(owner.Url);
            client.DefaultRequestHeaders.Add("HDR_Signature", owner.Signature);
            client.DefaultRequestHeaders.Add("HDR_IPAddress", owner.IPAddress);
            client.DefaultRequestHeaders.Add("HDR_UserName", owner.UserName);
            client.DefaultRequestHeaders.Add("HDR_Password", owner.Password);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(15)
        })
        .AddHttpMessageHandler<RetryDelegatingHandler>()
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
        return services;

    }
    public static IApplicationBuilder UseFettanEndpoint(this IApplicationBuilder builder, string endpoint = "/Awash")
    {

        //builder.UseSoapEndpoint<ITelebirrService, TelebirrCustomMessage>(endpoint, new SoapEncoderOptions(), SoapSerializer.XmlSerializer);
        return builder;
    }


}
