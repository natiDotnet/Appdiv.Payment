using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;

namespace Appdiv.Payment.CBEbirr;

public class CBEbirrPayment : ICBEbirrPayment
{
    public Task<ApplyTransactionResponse> PaymentQuery(ApplyTransactionRequest request)
    {
        return Task.FromResult(new ApplyTransactionResponse
        {
            BillRefNumber = "423323",
            Amount = 10.0m,
            CustomerName = "Sample Customer Name",
            TransID = "1wdcc",
            ShortCode = "123456"
        });
    }
}