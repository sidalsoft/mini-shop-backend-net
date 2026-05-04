using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mini_shop_backend_net.Application.DTOs;
using mini_shop_backend_net.Application.Services;
using mini_shop_backend_net.helper;

namespace mini_shop_backend_net.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductQuery query)
    {
        var result = await _service.GetAll(query);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _service.GetById(id);
        if (product == null) return NotFound();

        return Ok(product);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        await _service.Create(dto);
        return Created("", null);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Update(Guid id, CreateProductDto dto)
    {
        await _service.Update(id, dto);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}