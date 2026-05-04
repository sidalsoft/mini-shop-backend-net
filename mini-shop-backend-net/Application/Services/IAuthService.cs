using mini_shop_backend_net.Application.DTOs.User;

namespace mini_shop_backend_net.Application.Services;

public interface IAuthService
{
    Task<string> Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
}