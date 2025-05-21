using DirectPay.Application.Abstractions.Models;

namespace DirectPay.Application.Abstractions;

public interface ISettingRepository
{
    Task<Store<T>> AddAsync<T>(Store<T> store, CancellationToken cancellationToken = default);
    Task<Store<T>?> GetByKey<T>(string key, CancellationToken cancellationToken = default);
    Task<Store<T>?> ReadByKey<T>(string key, CancellationToken cancellationToken = default);
}
