using mini_shop_backend.Enums;

namespace mini_shop_backend;

public class User : BaseEntity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; }
}