using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public IQueryable<Product> Query()
    {
        return _db.AsQueryable();
    }
}