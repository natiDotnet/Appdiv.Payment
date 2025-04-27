using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DirectPay.Application.Abstration;
using DirectPay.Application.Shared;
using DirectPay.Application.Transations;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.API.Transactions;

[ApiController]
public class TransactionController(ITransactionRepository transactionRepository) : ControllerBase
{
    [HttpPost("initialize")]
    public async Task<ApiResponse> Initialize([FromBody] TransactionRequest request)
    {
        var transaction = request.ToModel();
        await transactionRepository.AddAsync(transaction);
        return ApiResponse.Success("Hosted Link");
    }

    [HttpPost("verify/{txRef}")]
    public async Task<ApiResponse> Verify([FromRoute] string txRef)
    {
        var transaction = await transactionRepository.GetByReferenceAsync(txRef);
        if (transaction == null)
            return ApiResponse.Error("Invalid transaction or Transaction not found");
        return ApiResponse.Success("Payment details", transaction.ToResponse());
    }

}