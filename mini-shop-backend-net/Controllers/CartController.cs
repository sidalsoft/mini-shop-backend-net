using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniShopBackendNet.Application.DTOs.Cart;
using miniShopBackendNet.Application.Services;

namespace miniShopBackendNet.Controllers;

[ApiController]
[Route("api/cart")]
[Authorize]
public class CartController(ICartService service) : ControllerBase
{

    private Guid GetUserId() => Guid.Parse(User.FindFirst("sub").Value);

    [HttpGet]
    public async Task<IActionResult> Get()
        => Ok(await service.GetCart(GetUserId()));

    [HttpPost]
    public async Task<IActionResult> Add(AddToCartDto dto)
    {
        await service.AddToCart(GetUserId(), dto);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCartDto dto)
    {
        await service.UpdateQuantity(GetUserId(), dto.ProductId, dto.Quantity);
        return NoContent();
    }

    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> Remove(Guid productId)
    {
        await service.Remove(GetUserId(), productId);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Clear()
    {
        await service.Clear(GetUserId());
        return NoContent();
    }
}