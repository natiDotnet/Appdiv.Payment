using DirectPay.Application.Abstractions.Models;

namespace DirectPay.Application.Abstractions;

public interface ISettingRepository
{
    Task<Store<T>> AddAsync<T>(Store<T> setting);
    Task<Store<T>?> GetByKey<T>(string key);
    Task<Store<T>?> ReadByKey<T>(string key);
}
