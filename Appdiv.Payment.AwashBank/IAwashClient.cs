using Appdiv.Payment.AwashBank.Contracts;

namespace Appdiv.Payment.AwashBank;

public interface IAwashClient
{
    Task<string> PingAsync();
    Task<TokenInfo> GetTokenAsync(Credential credential);
    Task<AccountInfo> AccountEnquireAsync(string account, string bankCode = "awsbnk");
    Task<TransactionDetail> TransferFundAsync(Transaction transaction);
    Task<TransactionDetail> ApproveOtpAsync(OtpInfo otp);
    Task<TransactionDetail> ApproveOtpAsync(string phone, string otp);
    Task<TransactionDetail> GetTransactionAsync(string reference);
    Task<Credential> ChangeCredentialAsync(Credential credential);
}
