using mini_shop_backend_net.Application.DTOs.Order;
using mini_shop_backend_net.Application.Services;
using mini_shop_backend_net.helper;
using mini_shop_backend.Enums;

namespace mini_shop_backend_net.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin/orders")]
[Authorize(Roles = Roles.Admin)]
public class AdminOrderController : ControllerBase
{
    private readonly IOrderService _service;

    public AdminOrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] OrderQuery query)
    {
        var result = await _service.GetAllOrders(query);
        return Ok(result);
    }
}