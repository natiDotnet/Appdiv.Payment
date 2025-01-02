using System.Net.Http.Json;
using Appdiv.Payment.Fettan.Requests;
using Appdiv.Payment.Fettan.Responses;
using Microsoft.Extensions.Options;

namespace Appdiv.Payment.Fettan;

public class FettanClient : IFettanClient
{
    private readonly FettanOptions _merchant;
    private readonly HttpClient _httpClient;

    public FettanClient(IOptionsMonitor<FettanOptions> options, HttpClient httpClient)
    {
        _merchant = options?.CurrentValue ?? throw new ArgumentNullException(nameof(options));
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    private async Task<ApiResponse> SendRequestAsync(RequestBody request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        request.MerchantID = _merchant.MerchantID;
        var response = await _httpClient.PostAsJsonAsync(_merchant.Url, request);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<ApiResponse>()
               ?? throw new InvalidOperationException("Failed to parse response.");
    }

    public Task<ApiResponse> AuthorizationAsync(string cardNumber, string sourceTransID = "")
    {
        var paymentInfo = new PaymentInfo
        {
            CardNumber = cardNumber ?? throw new ArgumentNullException(nameof(cardNumber)),
            SourceTransID = sourceTransID,
        };
        return HandlePaymentAsync(paymentInfo, PaymentAction.Authorization);
    }

    public Task<ApiResponse> DepositAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Deposit, additionalInfo1: additionalInfo1, additionalInfo5);
    }

    public Task<ApiResponse> RefundAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Refund, additionalInfo1: additionalInfo1);
    }

    public Task<ApiResponse> SaleAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo2 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Sale, additionalInfo1: additionalInfo1, additionalInfo2);
    }

    public Task<ApiResponse> WithdrawalAsync(PaymentInfo paymentInfo, string? additionalInfo1 = null, string? additionalInfo5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Withdraw, additionalInfo1: additionalInfo1, additionalInfo5);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, string vendorCode, string? additionalInfo1 = null, string? additionalInfo2 = null, string? additionalInfo3 = null, string? additionalInfo4 = null, string? additionalInfo5 = null)
    {
        if (paymentInfo is null)
        {
            throw new ArgumentNullException(nameof(paymentInfo));
        }
        var request = (RequestBody)paymentInfo; // Implicit conversion
        request.PaymentAction = action.ToString("D2");
        request.VendorAccount = vendorCode;
        request.AdditionalInfo1 = additionalInfo1 ?? string.Empty;
        request.AdditionalInfo2 = additionalInfo2 ?? string.Empty;
        request.AdditionalInfo3 = additionalInfo3 ?? string.Empty;
        request.AdditionalInfo4 = additionalInfo4 ?? string.Empty;
        request.AdditionalInfo5 = additionalInfo5 ?? string.Empty;

        return SendRequestAsync(request);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, string? additionalInfo1 = null, string? additionalInfo2 = null, string? additionalInfo3 = null, string? additionalInfo4 = null, string? additionalInfo5 = null)
    {
        return HandlePaymentAsync(paymentInfo, action, string.Empty, additionalInfo1, additionalInfo2, additionalInfo3, additionalInfo4, additionalInfo5);
    }

    public Task<ApiResponse> AirtimeAsync(PaymentInfo paymentInfo, AirtimeVendors vendorAccount = AirtimeVendors.ETC, AirtimeTransactionType? additionalInfo1 = null, string? additionalInfo2 = null, string? additionalInfo5 = null)
    {
        if (additionalInfo1 is null)
        {
            throw new ArgumentException("additionalInfo1 is required", nameof(additionalInfo1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.Airtime, vendorAccount.ToString(), additionalInfo1: additionalInfo1.ToString()?[0].ToString(), additionalInfo2, additionalInfo5);
    }

    public Task<ApiResponse> BillLookupAsync(PaymentInfo paymentInfo, VendorCode additionalInfo1, string additionalInfo2, string additionalInfo3)
    {
        if (string.IsNullOrWhiteSpace(additionalInfo2) || string.IsNullOrWhiteSpace(additionalInfo3))
        {
            throw new ArgumentException("additionalInfo1, additionalInfo2, and additionalInfo3 are required", nameof(additionalInfo1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillLookup, additionalInfo1: additionalInfo1.ToString(), additionalInfo2, additionalInfo3);
    }

    public Task<ApiResponse> BillPaymentAsync(PaymentInfo paymentInfo, string vendorAccount, string additionalInfo1, string additionalInfo2, string additionalInfo3 = null, string? additionalInfo4 = null, string? additionalInfo5 = null)
    {
        if (string.IsNullOrWhiteSpace(additionalInfo1) || string.IsNullOrWhiteSpace(additionalInfo2) || string.IsNullOrWhiteSpace(additionalInfo3))
        {
            throw new ArgumentException("additionalInfo1, additionalInfo2, and additionalInfo3 are required", nameof(additionalInfo1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillPayment, vendorAccount, additionalInfo1, additionalInfo2, additionalInfo3, additionalInfo4, additionalInfo5);
    }
}