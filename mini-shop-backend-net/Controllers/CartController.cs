using mini_shop_backend_net.Application.DTOs.Cart;
using mini_shop_backend_net.Application.Services;

namespace mini_shop_backend_net.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _service;

    public CartController(ICartService service)
    {
        _service = service;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirst("sub").Value);

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await _service.GetCart(GetUserId()));

    [HttpPost]
    public async Task<IActionResult> Add(AddToCartDto dto)
    {
        await _service.AddToCart(GetUserId(), dto);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCartDto dto)
    {
        await _service.UpdateQuantity(GetUserId(), dto.ProductId, dto.Quantity);
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> Remove(Guid productId)
    {
        await _service.Remove(GetUserId(), productId);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await _service.Clear(GetUserId());
        return NoContent();
    }
}