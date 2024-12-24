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
        var parameters = new List<Parameter>
        {
            new()
            {
                Key = nameof(response.BillRefNumber),
                Value = response.BillRefNumber ?? response.Parameters
                        .FirstOrDefault(p => p.Key == nameof(response.BillRefNumber))?.Value
                    ?? throw new MissingParameterException(nameof(response.BillRefNumber))
            },
            new()
            {
                Key = nameof(response.Amount),
                Value = response.Amount?.ToString() ?? response.Parameters
                        .FirstOrDefault(p => p.Key == nameof(response.Amount))?.Value
                    ?? throw new MissingParameterException(nameof(response.Amount))
            },
            new()
            {
                Key = nameof(response.CustomerName),
                Value = response.CustomerName ?? response.Parameters
                        .FirstOrDefault(p => p.Key == nameof(response.CustomerName))?.Value
                    ?? throw new MissingParameterException(nameof(response.CustomerName))
            }
        };
        if (response.UtilityName is not null && response.Parameters.Any(p => p.Key == nameof(response.UtilityName)))
            parameters.Add(new Parameter
            {
                Key = nameof(response.UtilityName),
                Value = response.UtilityName
            });

        if (response.TransID is not null && response.Parameters.Any(p => p.Key == nameof(response.TransID)))
            parameters.Add(new Parameter
            {
                Key = nameof(response.TransID),
                Value = response.TransID
            });
        response.Parameters = parameters.ToArray();
        return response;
    }
}