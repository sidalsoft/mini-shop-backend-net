using Microsoft.EntityFrameworkCore;
using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
    
    
    public IQueryable<Category> Query()
    {
        return _db.AsQueryable();
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _db.AnyAsync(c => c.Id == id);
    }
    
    public async Task<bool> ExistsByName(string name)
    {
        return await _db.AnyAsync(c => c.Name.ToLower() == name.ToLower());
    }

    public async Task<bool> HasProducts(Guid categoryId)
    {
        return await _context.Products.AnyAsync(p => p.CategoryId == categoryId);
    }
    
}