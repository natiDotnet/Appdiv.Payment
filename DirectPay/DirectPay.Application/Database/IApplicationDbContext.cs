using DirectPay.Domain.Settings;
using DirectPay.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Database;

public interface IApplicationDbContext
{
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Setting> Settings { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
