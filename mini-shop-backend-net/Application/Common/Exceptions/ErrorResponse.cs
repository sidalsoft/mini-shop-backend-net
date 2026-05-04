namespace mini_shop_backend_net.Application.Common.Exceptions;

public record ErrorResponse
(
    string ErrorCode,
    string Message,
    DateTime Timestamp,
    string Path
);