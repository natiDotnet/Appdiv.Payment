using System;
using System.Text.Json;
using DirectPay.Application.Abstractions;
using DirectPay.Application.Abstractions.Models;
using DirectPay.Application.Database;
using DirectPay.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Settings;

public class SettingRepository(IApplicationDbContext context) : ISettingRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Store<T>> AddAsync<T>(Store<T> store, CancellationToken cancellationToken = default)
    {
        var old = await _context.Settings
                .Where(s => s.Key == store.Key)
                .FirstOrDefaultAsync(cancellationToken);
        if (old is not null)
        {
            old.Configuration = JsonSerializer.Serialize(store.Value);
        }
        else
        {
            var setting = new Setting
            {
                Key = store.Key,
                Configuration = JsonSerializer.Serialize(store.Value),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _context.Settings.AddAsync(setting, cancellationToken);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return new Store<T>
        {
            Key = store.Key,
            Value = store.Value,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }

    public async Task<Store<T>?> GetByKey<T>(string key, CancellationToken cancellationToken = default)
    {
        var setting = await _context.Settings
            .Where(x => x.Key == key)
            .FirstOrDefaultAsync(cancellationToken);

        if (setting == null)
            return null;

        return new Store<T>
        {
            Key = setting.Key,
            Value = JsonSerializer.Deserialize<T>(setting.Configuration),
            CreatedAt = setting.CreatedAt,
            UpdatedAt = setting.UpdatedAt
        };
    }

    public async Task<Store<T>?> ReadByKey<T>(string key, CancellationToken cancellationToken = default)
    {
        var setting = await _context.Settings
            .Where(x => x.Key == key)
            .FirstOrDefaultAsync(cancellationToken);

        if (setting == null)
            return null;

        return new Store<T>
        {
            Key = setting.Key,
            Value = JsonSerializer.Deserialize<T>(setting.Configuration),
            CreatedAt = setting.CreatedAt,
            UpdatedAt = setting.UpdatedAt
        };
    }
}
