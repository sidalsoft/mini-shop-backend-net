using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mini_shop_backend_net.Application.DTOs.Category;
using mini_shop_backend_net.Application.Services;
using mini_shop_backend_net.helper;

namespace mini_shop_backend_net.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _service.GetById(id);
        if (category == null) return NotFound();

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        await _service.Create(dto);
        return StatusCode(201);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Update(Guid id, CreateCategoryDto dto)
    {
        await _service.Update(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return NoContent();
    }
}