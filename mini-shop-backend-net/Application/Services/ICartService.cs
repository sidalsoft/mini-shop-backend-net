using mini_shop_backend_net.Application.DTOs.Cart;

namespace mini_shop_backend_net.Application.Services;

public interface ICartService
{
    Task<CartResponse> GetCart(Guid userId);

    Task AddToCart(Guid userId, AddToCartDto dto);

    Task UpdateQuantity(Guid userId, Guid productId, int quantity);

    Task Remove(Guid userId, Guid productId);

    Task Clear(Guid userId);
}