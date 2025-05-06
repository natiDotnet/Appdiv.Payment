using System;
using DirectPay.Application.Abstration;
using DirectPay.Application.Database;
using DirectPay.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Settings;

public class SettingRepository(IApplicationDbContext context) : ISettingRepository
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Setting> AddAsync(Setting setting)
    {
        await _context.Settings.AddAsync(setting);
        await _context.SaveChangesAsync();

        return setting;
    }

    public Task<Setting?> GetByKey(string key)
    {
        return _context.Settings.FirstOrDefaultAsync(x => x.Key == key);
    }

    public async Task<Setting?> ReadByKey(string key)
    {
        return await _context.Settings
                            .AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Key == key);
    }



}
