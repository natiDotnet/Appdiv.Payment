using DirectPay.Application.Abstractions;
using DirectPay.Application.Abstractions.Models;
using DirectPay.Application.Database;
using DirectPay.Domain.Transactions;
using Microsoft.EntityFrameworkCore;

namespace DirectPay.Application.Transactions;


public class TransactionRepository : ITransactionRepository
{
    private readonly IApplicationDbContext _context;

    public TransactionRepository(IApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<int> AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        var transation = new Transaction
        {
            Amount = payment.Amount,
            Currency = payment.Currency,
            Email = payment.Email,
            FirstName = payment.FirstName,
            MiddleName = payment.MiddleName, // not require
            LastName = payment.LastName,
            PhoneNumber = payment.PhoneNumber,
            TxRef = payment.TxRef,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PaymentStatus = payment.PaymentStatus,
            PaymentType = payment.PaymentType,
            CallbackUrl = payment.CallbackUrl,
            ReturnUrl = payment.ReturnUrl,
            Reference = payment.Reference,
        };
        await _context.Transations.AddAsync(transation);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Payment?> ReadByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await _context.Transations
                            .AsNoTracking()
                            .Where(t => t.TxRef == reference)
                            .Select(t => new Payment
                            {
                                Amount = t.Amount,
                                Currency = t.Currency,
                                Email = t.Email,
                                FirstName = t.FirstName,
                                MiddleName = t.MiddleName, // not require
                                LastName = t.LastName,
                                PhoneNumber = t.PhoneNumber,
                                TxRef = t.TxRef,
                            })
                            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Payment?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await _context.Transations.Where(t => t.TxRef == reference)
                           .Select(t => new Payment
                           {
                               Amount = t.Amount,
                               Currency = t.Currency,
                               Email = t.Email,
                               FirstName = t.FirstName,
                               MiddleName = t.MiddleName, // not require
                               LastName = t.LastName,
                               PhoneNumber = t.PhoneNumber,
                               TxRef = t.TxRef,
                           })
                            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}