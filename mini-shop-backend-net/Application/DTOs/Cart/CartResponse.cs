namespace miniShopBackendNet.Application.DTOs.Cart;

public record CartResponse(
    Guid Id,
    List<CartItemResponse> Items,
    decimal Subtotal,
    decimal TotalPrice,
    int TotalItems
);