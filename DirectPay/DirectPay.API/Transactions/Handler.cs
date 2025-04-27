using System;
using DirectPay.Application.Abstration;
using DirectPay.Application.Settings;
using DirectPay.Application.Shared;
using DirectPay.Application.Transations;
using DirectPay.Domain.Transactions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.API.Transactions;

public static class Handler
{
    public static void Endpoint(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("v1/transaction");

        users.MapPost("/initialize", async ([FromBody] TransactionRequest request, ITransactionRepository transactionRepository) =>
        {
            var transaction = request.ToModel();
            await transactionRepository.AddAsync(transaction);
            return Results.Ok(ApiResponse.Success("Hosted Link"));
        });

        users.MapGet("/verify/{txRef}", async ([FromRoute] string txRef, ITransactionRepository transactionRepository) =>
        {
            var transaction = await transactionRepository.GetByReferenceAsync(txRef);
            if (transaction == null)
                return Results.NotFound(ApiResponse.Error("Invalid transaction or Transaction not found"));
            return Results.Ok(ApiResponse.Success("Payment details", transaction.ToResponse()));
        });
    }

}
