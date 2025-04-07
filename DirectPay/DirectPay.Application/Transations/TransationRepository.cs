using System;
using DirectPay.Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Transations;

public class TransationRepository : ITransationRepository
{
    private readonly ApplicationDbContext _context;

    public TransationRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Transation> AddAsync(Transation transation)
    {
        await _context.AddAsync(transation);
        await _context.SaveChangesAsync();

        return transation;
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


public interface ITransationRepository
{
    Task<Transation> AddAsync(Transation transation);
    Task<Transation?> GetByReferenceAsync(string reference);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}