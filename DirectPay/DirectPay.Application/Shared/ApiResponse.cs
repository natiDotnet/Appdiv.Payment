namespace DirectPay.Application.Shared;

public class ApiResponse
{
    public string Message { get; set; }
    public string Status { get; set; }
    public ApiResponse(string message, string status)
    {
        Message = message;
        Status = status;
    }
    public static ApiResponse Success(string message)
    {
        return new ApiResponse(message, "success");
    }
    public static ApiResponse Error(string message)
    {
        return new ApiResponse(message, "failed");
    }

    public static ApiResponse<TData> Success<TData>(string message, TData? data = default)
    {
        return new ApiResponse<TData>(message, "success", data);
    }

    public static ApiResponse<TData> Error<TData>(string message, TData? data = default)
    {
        return new ApiResponse<TData>(message, "failed", data);
    }
}

public class ApiResponse<TData> : ApiResponse
{
    public TData? Data { get; set; }
    public ApiResponse(string message, string status, TData? data) : base(message, status)
    {
        Data = data;
    }
}
