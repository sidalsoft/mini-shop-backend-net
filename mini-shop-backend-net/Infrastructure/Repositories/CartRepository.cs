using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

public class CartRepository : Repository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId)
    {
        return await _db
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart?> GetWithItemsAsync(Guid userId)
    {
        return await _db
            .Include(c => c.Items)
            .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<Cart> GetOrCreateAsync(Guid userId, bool ignoreDeletedProduct = true)
    {
        Cart? cart;
        if (ignoreDeletedProduct)
        {
            cart = await _db.Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        else
        {
            cart = await _db.Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }


        if (cart != null)
            return cart;

        cart = new Cart
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Items = new List<CartItem>()
        };

        await _db.AddAsync(cart);
        await _context.SaveChangesAsync();

        return cart;
    }
}