namespace miniShopBackendNet.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _db;

    public Repository(AppDbContext context)
    {
        _context = context;
        _db = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _db.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _db.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _db.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _db.Update(entity);
    }

    public void Delete(T entity)
    {
        _db.Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}