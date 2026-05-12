namespace miniShopBackendNet.Application.DTOs.Order;

public record OrderItemDto(
    Guid ProductId,
    int Quantity,
    string ProductName,
    decimal Price
);