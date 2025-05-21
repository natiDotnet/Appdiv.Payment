using DirectPay.Application.Abstractions.Models;

namespace DirectPay.Application.Abstractions;

public interface ITransactionRepository
{
    Task<int> AddAsync(Payment payment, CancellationToken cancellationToken = default);
    Task<Payment?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default);
    Task<Payment?> ReadByReferenceAsync(string reference, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}