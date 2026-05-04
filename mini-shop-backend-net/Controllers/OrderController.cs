using mini_shop_backend_net.Application.DTOs.Order;
using mini_shop_backend_net.Application.Services;

namespace mini_shop_backend_net.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    private Guid GetUserId() => Guid.Parse(User.FindFirst("sub").Value);

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var orderId = await _service.CreateOrder(GetUserId(), dto);
        return Ok(new { orderId });
    }

    [HttpGet]
    public async Task<IActionResult> GetMyOrders()
    {
        return Ok(await _service.GetUserOrders(GetUserId()));
    }
}