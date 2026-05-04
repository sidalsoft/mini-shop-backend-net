using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    IQueryable<Category> Query(); 
    Task<bool> Exists(Guid id);
    
    Task<bool> ExistsByName(string name);
    Task<bool> HasProducts(Guid categoryId);
}