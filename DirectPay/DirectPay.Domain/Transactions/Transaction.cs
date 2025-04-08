namespace DirectPay.Domain.Transactions;

public class Transaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Currency { get; set; } = "ETB";
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? TxRef { get; set; }
    public Uri? CallbackUrl { get; set; }
    public Uri? ReturnUrl { get; set; }
    public Customization? Customization { get; set; }
    public bool PaymentStatus { get; set; }
    public string? TransactionId { get; set; }
    public string? PaymentType { get; set; }
    public string? PaymentMethod { get; set; }
    public DateTime? PaymentDate { get; set; }

}

public class Customization
{
    public string? Titile { get; set; }
    public string? Description { get; set; }
}
