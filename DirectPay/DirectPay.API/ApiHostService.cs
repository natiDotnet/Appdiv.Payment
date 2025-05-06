using DirectPay.API.Plugins;
using DirectPay.API.Services;
using DirectPay.API.Transactions;
using DirectPay.Application;
using DirectPay.Application.Abstration;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DirectPay.API;
public class ApiHostService : BackgroundService
{
    private WebApplication? _app;
    private readonly IServiceProvider _services;
    private readonly ILogger<ApiHostService> _logger;
    private readonly string _pluginPath;

    public ApiHostService(IServiceProvider services, ILogger<ApiHostService> logger, IConfiguration config)
    {
        _services = services;
        _logger = logger;
        _pluginPath = Path.Combine(AppContext.BaseDirectory, "Plugins");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await StartAppAsync();

        using var watcher = new FileSystemWatcher(_pluginPath)
        {
            EnableRaisingEvents = true,
            IncludeSubdirectories = true,
            NotifyFilter = NotifyFilters.FileName

                        | NotifyFilters.LastWrite

                        | NotifyFilters.CreationTime
                        | NotifyFilters.DirectoryName
                        | NotifyFilters.Size
                        | NotifyFilters.Attributes
        };

        // Watch for all relevant directory and file changes
        watcher.Created += async (s, e) => await HandlePluginChange(e, "created");
        watcher.Changed += async (s, e) => await HandlePluginChange(e, "modified");
        watcher.Deleted += async (s, e) => await HandlePluginChange(e, "deleted");
        watcher.Renamed += async (s, e) => await HandlePluginChange(e, "renamed");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task HandlePluginChange(FileSystemEventArgs e, string changeType)
    {
        // Only restart if a .dll file was changed
        if (Path.GetExtension(e.FullPath).Equals(".dll", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogInformation("Plugin {ChangeType}: {Plugin}", changeType, e.Name);
            await RestartAppAsync();
        }
    }

    private async Task StartAppAsync()
    {
        var builder = WebApplication.CreateBuilder();

        // Configure Serilog

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/directpay-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Host.UseSerilog();
        builder.WebHost.UseUrls("http://localhost:2122", "https://localhost:7172");

        builder.Services.AddApplication(builder.Configuration);
        builder.Services.AddScoped<IConfigurationService, ConfigurationService>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddControllers()
            .PartManager
            .ApplicationParts
            .Add(new AssemblyPart(typeof(Handler).Assembly));

        // Load plugin assemblies

        var pluginAssemblies = PluginBootstrapper.LoadAssemblies(_pluginPath)
                                                 .ToList();
        PluginBootstrapper.PluginStartups = pluginAssemblies.SelectMany(PluginBootstrapper.GetPluginStartup);

        // Hook services with the correct logger

        await PluginBootstrapper.ApplyConfigureServices(
            builder.Services,
            builder.Configuration,
            pluginAssemblies);

        var app = builder.Build();
        // Configure the HTTP request pipeline.
        app.UseHttpsRedirection();

        // Hook middleware with the app's logger factory
        app.UseRouting();
        await PluginBootstrapper.ApplyConfigureMiddleware(
            app,
            builder.Configuration,
            pluginAssemblies);

        // Configure the HTTP request pipeline.
        app.UseSerilogRequestLogging(); // Add request logging middleware

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "DirectPay API V1");
            c.RoutePrefix = "docs"; // Changed from "docs" to the standard "swagger"
        });

        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();
        // app.MapControllers();

        _ = Task.Run(() => app.RunAsync());
        _app = app;
    }

    private async Task RestartAppAsync()
    {
        await _app?.StopAsync()!;
        Log.CloseAndFlush();
        // await _app.StartAsync();

        await StartAppAsync();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
        Log.CloseAndFlush(); // Ensure all logs are written before shutdown
    }
}
