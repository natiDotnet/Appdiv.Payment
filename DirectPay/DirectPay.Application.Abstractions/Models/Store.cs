namespace DirectPay.Application.Abstractions.Models;

public class Store<T>
{
    public Guid Id { get; set; }
    public required string Key { get; set; }
    public required T? Value { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
