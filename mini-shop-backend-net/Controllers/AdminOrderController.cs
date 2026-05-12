using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using miniShopBackendNet.Application.DTOs.Order;
using miniShopBackendNet.Application.Services;
using miniShopBackendNet.helper;

namespace miniShopBackendNet.Controllers;

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