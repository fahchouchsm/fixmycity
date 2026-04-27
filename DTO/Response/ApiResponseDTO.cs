namespace fixmycity.dto.Response;

public class ApiResponseDTO<T>
{
    public string Message { get; set; } = null!;
    public T? Data { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ApiResponseDTO(string message, T? data)
    {
        Message = message;
        Data = data;
    }

    public static ApiResponseDTO<object> Success(string message)
        => new ApiResponseDTO<object>(message, null);

    public static ApiResponseDTO<T> Success<T>(string message, T data)
        => new ApiResponseDTO<T>(message, data);

    public static ApiResponseDTO<object> Error(string message)
        => new ApiResponseDTO<object>(message, null);
}