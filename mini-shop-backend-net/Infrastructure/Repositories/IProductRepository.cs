using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> Query();
}