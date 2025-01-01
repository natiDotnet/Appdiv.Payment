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

    public Task<ApiResponse> DepositAsync(PaymentInfo paymentInfo, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Deposit, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    public Task<ApiResponse> RefundAsync(PaymentInfo paymentInfo, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Refund, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    public Task<ApiResponse> SaleAsync(PaymentInfo paymentInfo, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Sale, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    public Task<ApiResponse> WithdrawalAsync(PaymentInfo paymentInfo, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Withdraw, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, string vendorCode, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        if (paymentInfo is null)
        {
            throw new ArgumentNullException(nameof(paymentInfo));
        }
        var request = (RequestBody)paymentInfo; // Implicit conversion
        request.PaymentAction = action.ToString("D2");
        request.VendorAccount = vendorCode;
        request.AdditionalInfo1 = additionalInformation1 ?? string.Empty;
        request.AdditionalInfo2 = additionalInformation2 ?? string.Empty;
        request.AdditionalInfo3 = additionalInformation3 ?? string.Empty;
        request.AdditionalInfo4 = additionalInformation4 ?? string.Empty;
        request.AdditionalInfo5 = additionalInformation5 ?? string.Empty;

        return SendRequestAsync(request);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        return HandlePaymentAsync(paymentInfo, action, string.Empty, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    public Task<ApiResponse> AirtimeAsync(PaymentInfo paymentInfo, AirtimeVendors vendorAccount = AirtimeVendors.ETC, string? additionalInformation1 = null, string? additionalInformation2 = null, string? additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        if (string.IsNullOrWhiteSpace(additionalInformation1))
        {
            throw new ArgumentException("additionalInformation1 is required", nameof(additionalInformation1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.Airtime, vendor.ToString(), additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }

    public Task<ApiResponse> BillLookupAsync(PaymentInfo paymentInfo, VendorCode additionalInformation1, string additionalInformation2, string additionalInformation3)
    {
        if (string.IsNullOrWhiteSpace(additionalInformation1) || string.IsNullOrWhiteSpace(additionalInformation2) || string.IsNullOrWhiteSpace(additionalInformation3))
        {
            throw new ArgumentException("additionalInformation1, additionalInformation2, and additionalInformation3 are required", nameof(additionalInformation1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillLookup, vendorAccount, additionalInformation1, additionalInformation2, additionalInformation3);
    }

    public Task<ApiResponse> BillPaymentAsync(PaymentInfo paymentInfo, string vendorAccount, string additionalInformation1, string additionalInformation2, string additionalInformation3 = null, string? additionalInformation4 = null, string? additionalInformation5 = null)
    {
        if (string.IsNullOrWhiteSpace(additionalInformation1) || string.IsNullOrWhiteSpace(additionalInformation2) || string.IsNullOrWhiteSpace(additionalInformation3))
        {
            throw new ArgumentException("additionalInformation1, additionalInformation2, and additionalInformation3 are required", nameof(additionalInformation1));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillPayment, vendorAccount, additionalInformation1, additionalInformation2, additionalInformation3, additionalInformation4, additionalInformation5);
    }
}