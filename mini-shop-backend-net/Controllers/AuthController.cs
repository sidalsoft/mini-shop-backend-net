using Microsoft.AspNetCore.Mvc;
using mini_shop_backend_net.Application.DTOs.User;
using mini_shop_backend_net.Application.Services;

namespace mini_shop_backend_net.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
        _auth = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var token = await _auth.Register(dto);
        return Ok(new { token });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var token = await _auth.Login(dto);
        return Ok(new { token });
    }
}