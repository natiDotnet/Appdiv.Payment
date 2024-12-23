using Appdiv.Payment.CBEbirr.Exceptions;
using Appdiv.Payment.CBEbirr.Requests;
using Appdiv.Payment.CBEbirr.Responses;

namespace Appdiv.Payment.CBEbirr.Services;

// ReSharper disable once InconsistentNaming
public class CBEbirrService : ICBEbirrService
{
    private readonly ICBEbirrPayment _payment;

    public CBEbirrService(ICBEbirrPayment payment)
    {
        _payment = payment;
    }
    public async Task<ApplyTransactionResponse> C2BPaymentQueryRequest(Header Header, Body Body)
    {
        var request = new ApplyTransactionRequest
        {
            Header = Header,
            Body = Body
        };
        var response = await _payment.PaymentQuery(request);
        var requiredKeys = new[] { "BillRefNumber", "Amount", "CustomerName" };

        var missingKey = requiredKeys.FirstOrDefault(key => response.Parameters.All(p => p.Key != key));
        if (missingKey != null)
        {
            throw new MissingParameterException(missingKey);
        }
        return response;
    }
}
