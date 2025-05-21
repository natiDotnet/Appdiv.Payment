using Appdiv.Payment.AwashBank;
using Appdiv.Payment.AwashBank.Contracts;
using DirectPay.Application.Abstractions;

namespace DirectPay.AwashBank;

public class AwashPayment(IAwashClient client) : IOneTimePayment
{
    private readonly IAwashClient client = client;
    public async Task<bool> Transfer(string otp, string phoneNumber, decimal amount)
    {
        var response = await client.ApproveOtpAsync(
        new OtpInfo
        {
            Otp = otp,
            Phone = phoneNumber
        });
        return response.TransactionStatus == "Completed";
    }

    public async Task<string> Initiate(string phoneNumber, decimal amount)
    {
        Transaction transaction = new()
        {
            Amount = amount,
            AwashAccount = phoneNumber,
        };
        var response = await client.TransferFundAsync(transaction);
        return response.TransactionReference;
    }

}
