using miniShopBackendNet;
using miniShopBackendNet.Domain;

namespace miniShopBackendNet.Infrastructure.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IQueryable<Category> Query(); 
    Task<bool> Exists(Guid id);
    
    Task<bool> ExistsByName(string name);
    Task<bool> HasProducts(Guid categoryId);
}