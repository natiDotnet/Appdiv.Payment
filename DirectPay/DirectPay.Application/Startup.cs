using System;
using DirectPay.Application.Database;
using DirectPay.Application.Settings;
using DirectPay.Application.Transations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DirectPay.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<ApiKeySettings>(nameof(ApiKeySettings)).BindConfiguration(nameof(ApiKeySettings));
        services.AddOptions<DatabaseSettings>(nameof(DatabaseSettings)).BindConfiguration(nameof(DatabaseSettings));

        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddDatabase(configuration);

        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DirectPay");

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(connectionString, npgsqlOptions =>
                    npgsqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", nameof(DirectPay)))
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

}
