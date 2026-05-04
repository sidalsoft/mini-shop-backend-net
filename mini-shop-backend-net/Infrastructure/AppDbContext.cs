using mini_shop_backend;

namespace mini_shop_backend_net.Infrastructure;

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CartItem>()
            .HasIndex(x => new { x.CartId, x.ProductId })
            .IsUnique();
        
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<Category>().HasQueryFilter(x => x.DeletedAt == null);
        modelBuilder.Entity<Order>().HasQueryFilter(x => x.DeletedAt == null);
        modelBuilder.Entity<OrderItem>().HasQueryFilter(x => x.DeletedAt == null);
        modelBuilder.Entity<Product>().HasQueryFilter(x => x.DeletedAt == null);
        modelBuilder.Entity<User>().HasQueryFilter(x => x.DeletedAt == null);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }

            // 🔥 soft delete
            if (entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified;
                entry.Entity.DeletedAt = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}