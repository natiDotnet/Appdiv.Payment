using System;
using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;
using DirectPay.Application.Transations;

namespace DirectPay.Telebirr;

public class TelebirrPayment : ITelebirrPayment
{
    private readonly ITransationRepository transationRepository;
    public TelebirrPayment(ITransationRepository transationRepository)
    {
        this.transationRepository = transationRepository;
    }
    public async Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        var transation = await transationRepository.GetByReferenceAsync(request.BillRefNumber);
        if (transation is null || transation.Amount != request.TransAmount)
            return new C2BPaymentConfirmationResult
            {
                ResultCode = 0,
            };

        return new C2BPaymentConfirmationResult
        {
            ResultCode = 1,
        };
    }

    public async Task<C2BPaymentQueryResult> PaymentQueryAsync(C2BPaymentQueryRequest request)
    {
        var transation = await transationRepository.GetByReferenceAsync(request.BillRefNumber);
        if (transation is null)
            return new C2BPaymentQueryResult
            {
                Amount = 0m,
                ResultCode = 0,
                BillRefNumber = request.BillRefNumber,
                ResultDesc = "No Payment Found",
                TransID = request.TransID,
            };

        return new C2BPaymentQueryResult
        {
            Amount = transation.Amount,
            ResultCode = 1,
            BillRefNumber = request.BillRefNumber,
            ResultDesc = "Success",
            TransID = request.TransID,
        };

    }

    public async Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        var transation = await transationRepository.GetByReferenceAsync(request.BillRefNumber);
        if (transation is null)
            return new C2BPaymentValidationResult
            {
                ResultCode = 0,
                ResultDesc = "No Payment Found",
            };

        await transationRepository.SaveChangesAsync();

        if (transation.Amount != request.TransAmount)
            return new C2BPaymentValidationResult
            {
                ResultCode = 0,
                ResultDesc = "Amount Mismatch",
            };

        transation.TransactionId = request.TransID;
        transation.PaymentStatus = true;
        transation.PaymentDate = DateTime.Now;
        transation.PaymentMethod = nameof(Telebirr);

        return new C2BPaymentValidationResult
        {
            ResultCode = 1,
            ResultDesc = "Success",
            ThirdPartyTransID = request.TransID,
        };
    }
}
