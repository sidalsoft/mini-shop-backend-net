using mini_shop_backend_net.Domain;
using mini_shop_backend_net.Domain.Enums;

namespace mini_shop_backend_net.Domain;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}