using Microsoft.EntityFrameworkCore;
using miniShopBackendNet.Domain;
using miniShopBackendNet.Domain.Enums;

namespace miniShopBackendNet.Infrastructure;

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
        var email = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        var password = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");
        var admin = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            Role = UserRole.ROLE_ADMIN,
            CreatedAt = DateTime.UtcNow
        };

        admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        context.Users.Add(admin);
        await context.SaveChangesAsync();
    }
}