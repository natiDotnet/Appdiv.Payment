using System;
using System.Diagnostics;
using DirectPay.Application.Abstration;
using DirectPay.Application.Database;
using DirectPay.Application.Settings;
using DirectPay.Application.Transations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DirectPay.Application;

public static class Startup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiKeySettings>(
            configuration.GetSection(nameof(ApiKeySettings)));
        services.Configure<DatabaseSettings>(
            configuration.GetSection(nameof(DatabaseSettings)));
        services.AddScoped<ISettingRepository, SettingRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddDatabase(configuration);

        return services;
    }
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            (provider, options) =>
            {
                var config = provider.GetRequiredService<IOptionsSnapshot<DatabaseSettings>>().Value;
                if (config.DatabaseType is DatabaseType.None)
                {
                    return; // Skip adding the DbContext if DatabaseType is None or defaul
                }
                var connectionString = config.ConnectionString();
                options.UseDatabase(config.DatabaseType, connectionString);
            });

        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static string ConnectionString(this DatabaseSettings dbSettings)
    => dbSettings.DatabaseType switch
    {
        DatabaseType.PostgreSQL => $"Host={dbSettings.Host};Port={dbSettings.Port};Database={dbSettings.Database};Username={dbSettings.Username};Password={dbSettings.Password}",
        DatabaseType.SqlServer => $"Server={dbSettings.Host},{dbSettings.Port};Database={dbSettings.Database};User Id={dbSettings.Username};Password={dbSettings.Password};",
        DatabaseType.MySql => $"Server={dbSettings.Host};Database={dbSettings.Database};Uid={dbSettings.Username};Pwd={dbSettings.Password};Port={dbSettings.Port};",
        _ => throw new ArgumentOutOfRangeException(nameof(dbSettings), dbSettings, null)
    };

    public static async Task TestConnection(this DatabaseSettings databaseType)
    {
        var connectionString = databaseType.ConnectionString();
        switch (databaseType.DatabaseType)
        {
            case DatabaseType.PostgreSQL:
                using (var conn = new Npgsql.NpgsqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                }
                break;

            case DatabaseType.SqlServer:
                using (var conn = new Microsoft.Data.SqlClient.SqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                }
                break;

            case DatabaseType.MySql:
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection(connectionString))
                {
                    await conn.OpenAsync();
                }
                break;
        }

    }

    private static DbContextOptionsBuilder UseDatabase(this DbContextOptionsBuilder options, DatabaseType databaseType, string connectionString)
        => databaseType switch
        {
            DatabaseType.PostgreSQL => options.UsePostgres(connectionString),
            DatabaseType.SqlServer => options.UseSqlServer(connectionString),
            DatabaseType.MySql => options.UseMySql(connectionString),
            _ => throw new ArgumentOutOfRangeException(nameof(databaseType), databaseType, null)
        };

    private static DbContextOptionsBuilder UsePostgres(this DbContextOptionsBuilder options, string connectionString)
        => options
            .UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.EnableRetryOnFailure())
            .UseSnakeCaseNamingConvention();

    private static DbContextOptionsBuilder UseSqlServer(this DbContextOptionsBuilder options, string connectionString)
        => options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.MigrationsHistoryTable("__EFMigrationsHistory", nameof(DirectPay)));

    private static DbContextOptionsBuilder UseMySql(this DbContextOptionsBuilder options, string connectionString)
        => options.UseMySql(
            connectionString, ServerVersion.AutoDetect(connectionString),
            mySqlOptions => mySqlOptions
                .MigrationsHistoryTable("__EFMigrationsHistory", nameof(DirectPay))
                .EnableRetryOnFailure());
}
