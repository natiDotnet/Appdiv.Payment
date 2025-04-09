using System;
using DirectPay.Application.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.Application.Transations.Initialize;

public static class Handler
{
    public static void Endpoint(this IEndpointRouteBuilder routes)
    {
        var users = routes.MapGroup("v1/transaction");

        users.MapGet("/initialize", ([FromBody] TransactionRequest request, ITransactionRepository transactionRepository, ISettingRepository settingRepository) =>
        {
            var transaction = request.ToModel();
            transactionRepository.AddAsync(transaction);
            return Results.Ok();
        });
    }

}
