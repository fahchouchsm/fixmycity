namespace fixmycity.dto.Response;

public class ApiErrorResponse
{
    public string Message { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public ApiErrorResponse(string message)
    {
        Message = message;
    }

    public static ApiErrorResponse Fail(string message)
        => new ApiErrorResponse(message);
}