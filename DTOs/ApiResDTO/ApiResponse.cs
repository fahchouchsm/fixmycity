namespace fixmycity.dto.Response;

public class ApiResponse<T>
{ 
    public string? Message { get; set; }
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ApiResponse() { }

    public ApiResponse(T data)
    {
        Data = data;
    }

    public ApiResponse(string message, T? data = default)
    {
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> Ok(T data)
        => new ApiResponse<T>(data);

    public static ApiResponse<T> Ok(string message, T data)
        => new ApiResponse<T>(message, data);
}
