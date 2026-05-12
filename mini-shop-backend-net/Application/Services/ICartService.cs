using miniShopBackendNet.Application.DTOs.Cart;

namespace miniShopBackendNet.Application.Services;

public interface ICartService
{
    Task<CartResponse> GetCart(Guid userId);

    Task AddToCart(Guid userId, AddToCartDto dto);

    Task UpdateQuantity(Guid userId, Guid productId, int quantity);

    Task Remove(Guid userId, Guid productId);

    Task Clear(Guid userId);
}