namespace DirectPay.Cbebirr.Payments;

public class CbeOptions
{
    public string BasePath { get; set; } = "CBE";
    public string PaymentQueryPath { get; set; } = "PaymentQuery";
    public string PaymentConfirmationPath { get; set; } = "PaymentConfirmation";
    public string PaymentValidationPath { get; set; } = "PaymentValidation";
}
