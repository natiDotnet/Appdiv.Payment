using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Application.Abstration;

public abstract class PluginStartup
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract string Version { get; }

    public virtual IServiceCollection AddPlugin(IServiceCollection services, IConfiguration configuration) => services;
    public virtual IApplicationBuilder UsePlugin(IApplicationBuilder app) => app;
    public virtual IApplicationBuilder UsePlugin(IApplicationBuilder app, IConfiguration configuration) => app;


}

