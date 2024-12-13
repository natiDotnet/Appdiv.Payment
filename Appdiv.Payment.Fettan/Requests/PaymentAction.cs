namespace Appdiv.Payment.Fettan.Requests;

public enum PaymentAction
{
    Sale = 1,
    Deposit = 2,
    Refund = 3,
    Withdraw = 5,
    Authorization = 9,
    Airtime = 20,
    BillLookup = 30,
    BillPayment = 31,
    ClaimSubmission = 40,
}
