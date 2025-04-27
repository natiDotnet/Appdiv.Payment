using DirectPay.Domain.Transactions;

namespace DirectPay.Application.Abstration;

public interface ITransactionRepository
{
    Task<int> AddAsync(Transaction transation);
    Task<Transaction?> GetByReferenceAsync(string reference);
    Task<Transaction?> ReadByReferenceAsync(string reference);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}