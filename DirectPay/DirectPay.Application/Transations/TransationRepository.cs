using System;
using DirectPay.Application.Abstration;
using DirectPay.Application.Database;
using DirectPay.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Transations;


public class TransactionRepository : ITransactionRepository
{
    private readonly IApplicationDbContext _context;

    public TransactionRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> AddAsync(Transaction transation)
    {
        await _context.Transations.AddAsync(transation);
        return await _context.SaveChangesAsync();
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