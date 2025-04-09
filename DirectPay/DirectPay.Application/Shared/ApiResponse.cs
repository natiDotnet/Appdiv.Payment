namespace DirectPay.Application.Shared;

public class ApiResponse
{
    public required string Message { get; set; }
    public required string Status { get; set; }
    public object? Data { get; set; } 

    public static ApiResponse Success(string message, object? data = null)
    {
        return new ApiResponse
        {
            Message = message,
            Status = "success",
            Data = data
        };
    }
    public static ApiResponse Error(string message, object? data = null)
    {
        return new ApiResponse
        {
            Message = message,
            Status = "failed",
            Data = data
        };
    }
}
