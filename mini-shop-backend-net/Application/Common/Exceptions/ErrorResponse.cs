namespace mini_shop_backend_net.Application.Common.Exceptions;

public class ErrorResponse
{
    public string ErrorCode { get; set; }
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }
    public string Path { get; set; }
}