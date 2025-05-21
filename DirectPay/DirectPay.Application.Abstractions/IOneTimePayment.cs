namespace DirectPay.Application.Abstractions;

public interface IOneTimePayment
{
    Task<string> Initiate(string phoneNumber, decimal amount);
    Task<bool> Transfer(string otp, string phoneNumber, decimal amount);
}