using System;
using Microsoft.Extensions.DependencyInjection;
using TeleBirrSDK;

namespace Appdiv.Payment.TelebirrClient;

public class Startup
{
    public static IServiceCollection AddTelebirrClient(IServiceCollection services)
    {
        // var tele = new TeleBirrSdk();
        return services;
    }

}
