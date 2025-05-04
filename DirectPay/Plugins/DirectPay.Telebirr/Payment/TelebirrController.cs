using Appdiv.Payment.Shared.Models;
using Appdiv.Payment.Telebirr;
using Microsoft.AspNetCore.Mvc;

namespace DirectPay.Telebirr.Payment;
[ApiController]
[Route("api/[controller]/[action]")]
public class TelebirrController : ControllerBase
{
    private readonly ITelebirrPayment telebirrPayment;
    public TelebirrController(ITelebirrPayment telebirrPayment)
    {
        this.telebirrPayment = telebirrPayment;
    }
    [HttpPost]
    public Task<C2BPaymentConfirmationResult> PaymentConfirmationAsync(C2BPaymentConfirmationRequest request)
    {
        return telebirrPayment.PaymentConfirmationAsync(request);
    }
    [HttpPost]
    public Task<C2BPaymentQueryResult> PaymentQueryAsync(C2BPaymentQueryRequest request)
    {
        return telebirrPayment.PaymentQueryAsync(request);
    }
    [HttpPost]
    public Task<C2BPaymentValidationResult> PaymentValidationAsync(C2BPaymentValidationRequest request)
    {
        return telebirrPayment.PaymentValidationAsync(request);
    }

}
