namespace mini_shop_backend_net.Application.DTOs.Cart;

public record CartItemResponse(
    Guid ProductId,
    string ProductName,
    decimal Price,
    string ImageUrl,
    int Quantity,
    decimal TotalPrice,
    bool IsAvailable
);