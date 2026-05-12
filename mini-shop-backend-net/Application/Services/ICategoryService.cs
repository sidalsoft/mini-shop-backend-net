using miniShopBackendNet.Application.DTOs.Category;

namespace miniShopBackendNet.Application.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAll();
    Task<CategoryDto?> GetById(Guid id);
    Task Create(CreateCategoryDto dto);
    Task Update(Guid id, CreateCategoryDto dto);
    Task Delete(Guid id);
}