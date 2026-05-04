namespace mini_shop_backend_net.Application.DTOs.Order;

public record OrderItemDto(
    Guid ProductId,
    int Quantity,
    string ProductName,
    decimal Price
);