using mini_shop_backend_net.Application.DTOs.Category;

namespace mini_shop_backend_net.Application.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
    Task<CategoryDto?> GetById(Guid id);
    Task Create(CreateCategoryDto dto);
    Task Update(Guid id, CreateCategoryDto dto);
    Task Delete(Guid id);
}