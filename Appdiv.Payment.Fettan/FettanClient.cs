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

    public Task<ApiResponse> DepositAsync(PaymentInfo paymentInfo, params string[] additionalInformation)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Deposit, additionalInformation);
    }

    public Task<ApiResponse> RefundAsync(PaymentInfo paymentInfo, params string[] additionalInformation)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Refund, additionalInformation);
    }

    public Task<ApiResponse> SaleAsync(PaymentInfo paymentInfo, params string[] additionalInformation)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Sale, additionalInformation);
    }

    public Task<ApiResponse> WithdrawalAsync(PaymentInfo paymentInfo, params string[] additionalInformation)
    {
        return HandlePaymentAsync(paymentInfo, PaymentAction.Withdraw, additionalInformation);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, string vendorCode, params string[] additionalInfo)
    {
        if (paymentInfo is null)
        {
            throw new ArgumentNullException(nameof(paymentInfo));
        }
        var request = (RequestBody)paymentInfo; // Implicit conversion
        request.PaymentAction = action.ToString("D2");
        request.VendorAccount = vendorCode;
        if (additionalInfo.Length <= 0) return SendRequestAsync(request);
        request.AdditionalInfo1 = additionalInfo[0];
        request.AdditionalInfo2 = additionalInfo.Length > 1 ? additionalInfo[1] : string.Empty;
        request.AdditionalInfo3 = additionalInfo.Length > 2 ? additionalInfo[2] : string.Empty;
        request.AdditionalInfo4 = additionalInfo.Length > 3 ? additionalInfo[3] : string.Empty;
        request.AdditionalInfo5 = additionalInfo.Length > 4 ? additionalInfo[4] : string.Empty;

        return SendRequestAsync(request);
    }

    private Task<ApiResponse> HandlePaymentAsync(PaymentInfo paymentInfo, PaymentAction action, params string[] additionalInfo)
    {
        return HandlePaymentAsync(paymentInfo, action, string.Empty, additionalInfo);
    }

    public Task<ApiResponse> AirtimeAsync(PaymentInfo paymentInfo, string vendor = AirtimeVendor.EthioTelecom, params string[] additionalInformation)
    {
        if (additionalInformation.Length < 1)
        {
            throw new ArgumentException("one additional information required", nameof(additionalInformation));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.Airtime, vendor, additionalInformation);
    }

    public Task<ApiResponse> BillLookupAsync(PaymentInfo paymentInfo, string vendorAccount, params string[] additionalInformation)
    {
        if (additionalInformation.Length < 1)
        {
            throw new ArgumentException("one additional information required", nameof(additionalInformation));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillLookup, vendorAccount, additionalInformation);
    }

    public Task<ApiResponse> BillPaymentAsync(PaymentInfo paymentInfo, string vendorAccount, params string[] additionalInformation)
    {
        if (additionalInformation.Length < 1)
        {
            throw new ArgumentException("one additional information required", nameof(additionalInformation));
        }
        return HandlePaymentAsync(paymentInfo, PaymentAction.BillPayment, vendorAccount, additionalInformation);
    }
}