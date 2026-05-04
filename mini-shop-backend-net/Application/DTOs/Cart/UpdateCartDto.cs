namespace mini_shop_backend_net.Application.DTOs.Cart;

public record UpdateCartDto(Guid ProductId, int Quantity);
