using System.ServiceModel;
using Appdiv.Payment.Shared.Helper;

namespace Appdiv.Payment.Shared.Models;

[MessageContract(IsWrapped = true, WrapperName = nameof(C2BPaymentQueryResult), WrapperNamespace = Namespace.C2B)]
public class C2BPaymentQueryResult
{
    public string ResultCode { get; set; } = string.Empty;

    public string ResultDesc { get; set; } = string.Empty;

    public string TransID { get; set; } = string.Empty;

    public string BillRefNumber { get; set; } = string.Empty;

    public string UtilityName { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}