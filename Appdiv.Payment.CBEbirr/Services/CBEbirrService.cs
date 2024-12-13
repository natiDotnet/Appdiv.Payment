using System;
using System.Linq;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using Appdiv.C2BPayment.CBEbirr.Responses;
using Appdiv.C2BPayment.CBEbirr.Requests;
using Appdiv.Payment.Telebirr.Requests;

namespace Appdiv.C2BPayment.CBEbirr.Services;

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
        return await _payment.PaymentQuery(request);
    }
}
