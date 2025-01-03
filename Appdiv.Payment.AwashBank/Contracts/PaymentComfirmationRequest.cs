﻿namespace Appdiv.Payment.AwashBank.Contracts;

public class PaymentConfirmationRequest
{
    public string PaymentCode { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string TransId { get; set; } = string.Empty;
}
