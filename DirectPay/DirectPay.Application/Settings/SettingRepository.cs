using System;
using DirectPay.Application.Database;
using DirectPay.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Settings;

public interface ISettingRepository
{
    Task<Setting> AddAsync(Setting setting);
    Task<Setting> GetByKey(string key);
    Task<Setting> ReadByKey(string key);
}
public class SettingRepository : ISettingRepository
{
    private readonly IApplicationDbContext _context;
    public SettingRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Setting> AddAsync(Setting setting)
    {
        await _context.Settings.AddAsync(setting);
        await _context.SaveChangesAsync();

        return setting;
    }

    public async Task<Setting> GetByKey(string key)
    {
        return await _context.Settings.FirstOrDefaultAsync(x => x.Key == key)
            ?? throw new KeyNotFoundException(nameof(key));
    }

    public async Task<Setting> ReadByKey(string key)
    {
        return await _context.Settings
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Key == key)
            ?? throw new KeyNotFoundException(nameof(key));
    }



}
