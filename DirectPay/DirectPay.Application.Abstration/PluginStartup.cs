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
    public virtual Task<IServiceCollection> AddPluginAsync(IServiceCollection services, IConfiguration configuration) => Task.FromResult(services);
    public virtual IApplicationBuilder UsePlugin(IApplicationBuilder app) => app;
    public virtual Task<IApplicationBuilder> UsePluginAsync(IApplicationBuilder app) => Task.FromResult(app);
    public virtual IApplicationBuilder UsePlugin(IApplicationBuilder app, IConfiguration configuration) => app;
    public virtual Task<IApplicationBuilder> UsePluginAsync(IApplicationBuilder app, IConfiguration configuration) => Task.FromResult(app);


}

