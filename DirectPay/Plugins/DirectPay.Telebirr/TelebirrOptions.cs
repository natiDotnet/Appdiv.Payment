namespace DirectPay.Telebirr;

public class TelebirrOptions
{
    public string BasePath { get; set; } = "/api/Telebirr";
    public string PaymentQueryPath { get; set; } = "PaymentQuery";
    public string PaymentConfirmationPath { get; set; } = "PaymentConfirmation";
    public string PaymentValidationPath { get; set; } = "PaymentValidation";
}
