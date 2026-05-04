using mini_shop_backend;

namespace mini_shop_backend_net.Infrastructure.Repositories.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> Query();
}