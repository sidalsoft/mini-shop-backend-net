using Microsoft.EntityFrameworkCore;
using mini_shop_backend_net.Infrastructure;

namespace mini_shop_backend_net.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(
                Environment.GetEnvironmentVariable("DB_CONNECTION")));
    }

    public static async Task ApplyMigrationsAsync(
        this WebApplication app)
    {
        var retries = 5;

        while (retries > 0)
        {
            try
            {
                using var scope = app.Services.CreateScope();

                var db = scope.ServiceProvider
                    .GetRequiredService<AppDbContext>();

                await db.Database.MigrateAsync();

                break;
            }
            catch
            {
                retries--;

                await Task.Delay(2000);
            }
        }
    }

    public static async Task SeedDatabaseAsync(
        this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        await DbSeeder.SeedAdminAsync(scope.ServiceProvider);
    }
}