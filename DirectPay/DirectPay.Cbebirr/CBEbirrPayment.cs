using System;
using Appdiv.Payment.CBEBirr;
using Appdiv.Payment.Shared.Models;
using DirectPay.Application.Transations;

namespace DirectPay.Cbebirr;

public class CBEbirrPayment : ICBEBirrPayment
{
    private readonly ITransationRepository transationRepository;
    public CBEbirrPayment(ITransationRepository transationRepository)
    {
        this.transationRepository = transationRepository;
    }
    public async Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        var transation = await transationRepository.ReadByReferenceAsync(request.BillRefNumber);
        if (transation is null || transation.Amount != request.TransAmount)
            return new C2BPaymentConfirmationResult
            {
                ResultCode = 1,
            };

        transation.TransactionId = request.TransID;
        transation.PaymentStatus = true;
        transation.PaymentDate = DateTime.Now;
        transation.PaymentMethod = nameof(Cbebirr);

        await transationRepository.SaveChangesAsync();

        return new C2BPaymentConfirmationResult
        {
            ResultCode = 0,
        };
    }

    public async Task<ApplyTransactionResponse> PaymentQueryAsync(ApplyTransactionRequest request)
    {
        var transation = await transationRepository.ReadByReferenceAsync(request.Body.BillReferenceNumber);
        if (transation is null)
            return new ApplyTransactionResponse
            {
                Amount = 0m,
                ResponseCode = 1,
                ResponseDesc = "No Payment Found",
            };

        return new ApplyTransactionResponse
        {
            Amount = transation.Amount,
            ResponseCode = 0,
        };
    }

    public async Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        var transation = await transationRepository.GetByReferenceAsync(request.BillRefNumber);
        if (transation is null)
            return new C2BPaymentValidationResult
            {
                ResultCode = 1,
                ResultDesc = "No Payment Found",
            };


        if (transation.Amount != request.TransAmount)
            return new C2BPaymentValidationResult
            {
                ResultCode = 1,
                ResultDesc = "Amount Mismatch",
            };

        await transationRepository.SaveChangesAsync();

        return new C2BPaymentValidationResult
        {
            ResultCode = 0,
            ResultDesc = "Success",
            ThirdPartyTransID = request.TransID,
        };
    }
}
