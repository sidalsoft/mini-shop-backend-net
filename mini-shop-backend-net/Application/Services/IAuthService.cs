using miniShopBackendNet.Application.DTOs.User;

namespace miniShopBackendNet.Application.Services;

public interface IAuthService
{
    Task<string> Register(RegisterDto dto);
    Task<string> Login(LoginDto dto);
}