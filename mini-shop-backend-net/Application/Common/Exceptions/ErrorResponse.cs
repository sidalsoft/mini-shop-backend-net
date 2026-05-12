namespace miniShopBackendNet.Application.Common.Exceptions;

public record ErrorResponse
(
    string ErrorCode,
    string Message,
    DateTime Timestamp,
    string Path
);