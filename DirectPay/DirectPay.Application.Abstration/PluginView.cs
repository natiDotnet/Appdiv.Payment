namespace DirectPay.Application.Abstration;

public class PluginView
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Title { get; set; }
    public required Type Component { get; set; }
    public string? Icon { get; set; }
}