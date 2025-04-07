using DirectPay.Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application;

public interface IApplicationDbContext
{
    DbSet<Transation> Transations { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
