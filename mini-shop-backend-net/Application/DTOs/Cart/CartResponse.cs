namespace mini_shop_backend_net.Application.DTOs.Cart;

public record CartResponse(
    Guid Id,
    List<CartItemResponse> Items,
    decimal Subtotal,
    decimal TotalPrice,
    int TotalItems
);