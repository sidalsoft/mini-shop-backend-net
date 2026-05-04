using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart?> GetByUserIdAsync(Guid userId);

    Task<Cart> GetOrCreateAsync(Guid userId, bool ignoreDeletedProduct = true);

    Task<Cart?> GetWithItemsAsync(Guid userId);
}