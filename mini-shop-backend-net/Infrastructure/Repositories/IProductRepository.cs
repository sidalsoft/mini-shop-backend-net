using miniShopBackendNet;
using miniShopBackendNet.Domain;

namespace miniShopBackendNet.Infrastructure.Repositories;

public interface IProductRepository : IRepository<Product>
{
    IQueryable<Product> Query();
}