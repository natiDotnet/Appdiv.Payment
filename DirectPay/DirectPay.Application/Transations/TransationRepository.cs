using System;
using DirectPay.Application.Database;
using DirectPay.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Transations;

public interface ITransationRepository
{
    Task<Transaction> AddAsync(Transaction transation);
    Task<Transaction?> GetByReferenceAsync(string reference);
    Task<Transaction?> ReadByReferenceAsync(string reference);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class TransationRepository : ITransationRepository
{
    private readonly IApplicationDbContext _context;

    public TransationRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Transaction> AddAsync(Transaction transation)
    {
        await _context.Transations.AddAsync(transation);
        await _context.SaveChangesAsync();

        return transation;
    }

    public async Task<Transaction?> ReadByReferenceAsync(string reference)
    {
        return await _context.Transations
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.TxRef == reference);
    }

    public async Task<Transaction?> GetByReferenceAsync(string reference)
    {
        return await _context.Transations.FirstOrDefaultAsync(t => t.TxRef == reference);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}