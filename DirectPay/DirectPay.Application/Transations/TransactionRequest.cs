using System;
using DirectPay.Domain.Transactions;

namespace DirectPay.Application.Transations;

public class TransactionRequest
{
    public decimal Amount { get; set; }
    public string? Currency { get; set; } = "ETB";
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TxRef { get; set; }
    public Uri? CallbackUrl { get; set; }
    public Uri? ReturnUrl { get; set; }
    public Customization? Customization { get; set; }
}
