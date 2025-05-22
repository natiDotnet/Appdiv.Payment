using DirectPay.Application.Abstractions;
using DirectPay.Application.Abstractions.Models;
using DirectPay.Application.Database;
using DirectPay.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Customization = DirectPay.Application.Abstractions.Models.Customization;

namespace DirectPay.Application.Transactions;


public class TransactionRepository(IApplicationDbContext context) : ITransactionRepository
{
    private readonly IApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<int> AddAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        var transaction = new Transaction
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
            Customization = new Domain.Transactions.Customization
            {
                Title = payment.Customization?.Title,
                Description = payment.Customization?.Description
            }
        };
        await _context.Transactions.AddAsync(transaction, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Payment?> ReadByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
                            .AsNoTracking()
                            .Where(t => t.TxRef == reference)
                            .Select(t => new Payment
                            {
                                Id = t.Id,
                                Amount = t.Amount,
                                Currency = t.Currency,
                                Email = t.Email,
                                FirstName = t.FirstName,
                                MiddleName = t.MiddleName,
                                LastName = t.LastName,
                                PhoneNumber = t.PhoneNumber,
                                TxRef = t.TxRef,
                                CallbackUrl = t.CallbackUrl,
                                ReturnUrl = t.ReturnUrl,
                                PaymentStatus = t.PaymentStatus,
                                Reference = t.Reference,
                                PaymentType = t.PaymentType,
                                PaymentMethod = t.PaymentMethod,
                                PaymentDate = t.PaymentDate,
                                CreatedAt = t.CreatedAt,
                                UpdatedAt = t.UpdatedAt,
                                Customization = t.Customization != null ? new Customization
                                {
                                    Title = t.Customization.Title,
                                    Description = t.Customization.Description
                                } : null
                            })
                            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Payment?> GetByReferenceAsync(string reference, CancellationToken cancellationToken = default)
    {
        return await _context.Transactions
                           .Where(t => t.TxRef == reference)
                           .Select(t => new Payment
                           {
                               Id = t.Id,
                               Amount = t.Amount,
                               Currency = t.Currency,
                               Email = t.Email,
                               FirstName = t.FirstName,
                               MiddleName = t.MiddleName,
                               LastName = t.LastName,
                               PhoneNumber = t.PhoneNumber,
                               TxRef = t.TxRef,
                               CallbackUrl = t.CallbackUrl,
                               ReturnUrl = t.ReturnUrl,
                               PaymentStatus = t.PaymentStatus,
                               Reference = t.Reference,
                               PaymentType = t.PaymentType,
                               PaymentMethod = t.PaymentMethod,
                               PaymentDate = t.PaymentDate,
                               CreatedAt = t.CreatedAt,
                               UpdatedAt = t.UpdatedAt,
                               Customization = t.Customization != null ? new Customization
                               {
                                   Title = t.Customization.Title,
                                   Description = t.Customization.Description
                               } : null
                           })
                            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}