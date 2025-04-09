using System;
using DirectPay.Domain.Transactions;

namespace DirectPay.Application.Transations;

public static class TransactionExtensions
{

    public static Transaction ToModel(this TransactionRequest request)
    {
        return new Transaction
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
            Customization = request.Customization,
        };
    }

}
