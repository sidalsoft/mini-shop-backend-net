using miniShopBackendNet.Domain;

namespace miniShopBackendNet.Infrastructure.Repositories;

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