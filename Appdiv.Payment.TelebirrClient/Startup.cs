using System;
using Appdiv.Payment.TelebirrClient.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TeleBirrSDK;

namespace Appdiv.Payment.TelebirrClient;

public class Startup
{
    public static IServiceCollection AddTelebirrClient(IServiceCollection services, TelebirrConfig? config = null)
    {
        // var tele = new TeleBirrSdk();
        if (config is null)
        {
            services.AddOptions<TelebirrConfig>();
        }
        services.AddHttpClient<ITelebirrClient, TelebirrClient>((serviceProvider, client) =>
        {
            config ??= serviceProvider.GetRequiredService<IOptions<TelebirrConfig>>().Value;
            client.BaseAddress = new Uri(config.BaseUrl);
            client.DefaultRequestHeaders.Add("X-APP-Key", config.FabricAppId);
        });
        return services;
    }

}
