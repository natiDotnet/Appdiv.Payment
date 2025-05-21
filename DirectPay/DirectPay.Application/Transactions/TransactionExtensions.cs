using DirectPay.Application.Abstractions.Models;

namespace DirectPay.Application.Transactions;

public static class TransactionExtensions
{

    public static Payment ToModel(this TransactionRequest request)
    {
        return new Payment
        {
            Amount = request.Amount,
            Currency = request.Currency,
            Email = request.Email,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            TxRef = request.TxRef,
            CallbackUrl = request.CallbackUrl?.ToString(),
            ReturnUrl = request.ReturnUrl?.ToString(),
            // Customization = request.Customization,
        };
    }

    public static TransactionResponse ToResponse(this Payment transaction)
    {
        return new TransactionResponse
        {
            Amount = transaction.Amount,
            Currency = transaction.Currency,
            Email = transaction.Email,
            FirstName = transaction.FirstName,
            MiddleName = transaction.MiddleName,
            LastName = transaction.LastName,
            PhoneNumber = transaction.PhoneNumber,
            Reference = transaction.Reference,
            TxRef = transaction.TxRef,
            CallbackUrl = transaction.CallbackUrl != null ? new Uri(transaction.CallbackUrl) : null,
            ReturnUrl = transaction.ReturnUrl != null ? new Uri(transaction.ReturnUrl) : null,
            // Customization = transaction.Customization,
        };
    }

}
