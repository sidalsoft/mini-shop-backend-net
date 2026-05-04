using mini_shop_backend;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(Guid userId);

    Task<Cart> GetOrCreateAsync(Guid userId, bool ignoreDeletedProduct = true);

    Task<Cart?> GetWithItemsAsync(Guid userId);
}