using Microsoft.EntityFrameworkCore;
using mini_shop_backend_net.Domain;
using mini_shop_backend_net.Domain.Enums;

namespace mini_shop_backend_net.Infrastructure;

public static class DbSeeder
{
    public static async Task SeedAdminAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var adminExists = await context.Users
            .AnyAsync(u => u.Role == UserRole.ROLE_ADMIN);

        if (adminExists)
            return;

        var admin = new User
        {
            Id = Guid.NewGuid(),
            Email = "admin@gmail.com",
            Role = UserRole.ROLE_ADMIN,
            CreatedAt = DateTime.UtcNow
        };

        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin");

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}