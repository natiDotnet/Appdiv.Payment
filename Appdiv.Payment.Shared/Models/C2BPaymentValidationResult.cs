using System.ServiceModel;
using Appdiv.Payment.Shared.Helper;

namespace Appdiv.Payment.Shared.Models;

[MessageContract(IsWrapped = true, WrapperName = nameof(C2BPaymentValidationResult), WrapperNamespace = Namespace.C2B)]
public class C2BPaymentValidationResult
{
    public int ResultCode { get; set; }
    public string ResultDesc { get; set; } = string.Empty;
    public string ThirdPartyTransID { get; set; } = string.Empty;
}