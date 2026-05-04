using mini_shop_backend_net.Infrastructure.Repositories.Repositories;
using mini_shop_backend;

namespace mini_shop_backend_net.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

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