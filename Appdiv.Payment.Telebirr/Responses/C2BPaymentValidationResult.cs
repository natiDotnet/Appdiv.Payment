using System.ServiceModel;

namespace Appdiv.Payment.Telebirr.Responses;

[MessageContract(IsWrapped = true, WrapperName = nameof(C2BPaymentValidationResult), WrapperNamespace = Namespace.C2B)]
public class C2BPaymentValidationResult
{
    public int ResultCode { get; set; }
    public string ResultDesc { get; set; } = string.Empty;
    public string ThirdPartyTransID { get; set; } = string.Empty;
}