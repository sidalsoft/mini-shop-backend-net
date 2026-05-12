using miniShopBackendNet.Application.Common.Exceptions;
using miniShopBackendNet.Application.DTOs.Cart;
using miniShopBackendNet.Infrastructure.Repositories;
using miniShopBackendNet.Domain;

namespace miniShopBackendNet.Application.Services;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepo;
    private readonly IProductRepository _productRepo;

    public CartService(
        ICartRepository cartRepo,
        IProductRepository productRepo)
    {
        _cartRepo = cartRepo;
        _productRepo = productRepo;
    }

    // -------------------- GET CART --------------------
    public async Task<CartResponse> GetCart(Guid userId)
    {
        var cart = await _cartRepo.GetOrCreateAsync(userId, false);

        var items = cart.Items.Select(i =>
        {
            var isAvailable = i.Product != null && i.Product.DeletedAt == null;

            return new CartItemResponse(
                i.ProductId,
                isAvailable ? i.Product.Name : "Товар удалён",
                 i.Product?.Price ?? 0,
                i.Product?.Description ?? "",
                i.Quantity,
                isAvailable ? i.Product.Price * i.Quantity : 0,
                isAvailable
            );
        }).ToList();

        var subtotal = items
            .Where(i => i.IsAvailable)
            .Sum(i => i.TotalPrice);

        var totalItems = items.Sum(i => i.Quantity);

        return new CartResponse(
            cart.Id,
            items,
            subtotal,
            subtotal,
            totalItems
        );
    }

    // -------------------- ADD --------------------
    public async Task AddToCart(Guid userId, AddToCartDto dto)
    {
        if (dto.Quantity <= 0)
            throw new AppException("Количество должно быть больше 0");

        var product = await _productRepo.GetByIdAsync(dto.ProductId);
        if (product == null || product.DeletedAt != null)
            throw new NotFoundException("Товар не найден");

        var cart = await _cartRepo.GetOrCreateAsync(userId);

        var item = cart.Items
            .FirstOrDefault(x => x.ProductId == dto.ProductId);

        if (item != null)
        {
                item.Quantity += dto.Quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                CartId = cart.Id,
                ProductId = product.Id,
                Quantity = dto.Quantity
            });
        }

        await _cartRepo.SaveChangesAsync();
    }

    // -------------------- UPDATE --------------------
    public async Task UpdateQuantity(Guid userId, Guid productId, int quantity)
    {
        var cart = await _cartRepo.GetOrCreateAsync(userId);

        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item == null)
            throw new NotFoundException("Товар не найден в корзине");

        if (quantity <= 0)
        {
            cart.Items.Remove(item);
        }
        else
        {
            item.Quantity = quantity;
        }

        await _cartRepo.SaveChangesAsync();
    }

    // -------------------- REMOVE --------------------
    public async Task Remove(Guid userId, Guid productId)
    {
        var cart = await _cartRepo.GetOrCreateAsync(userId,  false);

        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            await _cartRepo.SaveChangesAsync();
        }
    }

    // -------------------- CLEAR --------------------
    public async Task Clear(Guid userId)
    {
        var cart = await _cartRepo.GetOrCreateAsync(userId);
        cart.Items.Clear();
        await _cartRepo.SaveChangesAsync();
    }
}