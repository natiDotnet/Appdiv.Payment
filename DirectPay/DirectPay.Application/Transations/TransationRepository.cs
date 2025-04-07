using System;
using DirectPay.Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Transations;

public interface ITransationRepository
{
    Task<Transation> AddAsync(Transation transation);
    Task<Transation?> GetByReferenceAsync(string reference);
    Task<Transation?> ReadByReferenceAsync(string reference);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class TransationRepository : ITransationRepository
{
    private readonly IApplicationDbContext _context;

    public TransationRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Transation> AddAsync(Transation transation)
    {
        await _context.Transations.AddAsync(transation);
        await _context.SaveChangesAsync();

        return transation;
    }

    public async Task<Transation?> ReadByReferenceAsync(string reference)
    {
        return await _context.Transations
                            .AsNoTracking()
                            .FirstOrDefaultAsync(t => t.TxRef == reference);
    }

    public async Task<Transation?> GetByReferenceAsync(string reference)
    {
        return await _context.Transations.FirstOrDefaultAsync(t => t.TxRef == reference);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}