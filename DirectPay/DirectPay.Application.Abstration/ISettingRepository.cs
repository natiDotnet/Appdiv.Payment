using DirectPay.Domain.Settings;

namespace DirectPay.Application.Abstration;

public interface ISettingRepository
{
    Task<Setting> AddAsync(Setting setting);
    Task<Setting?> GetByKey(string key);
    Task<Setting?> ReadByKey(string key);
}