using miniShopBackendNet.Application.DTOs;

namespace miniShopBackendNet.Application.Services;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetAll(ProductQuery query);
    Task<ProductDto> GetById(Guid id);
    Task Create(CreateProductDto dto);
    Task Update(Guid id, CreateProductDto dto);
    Task Delete(Guid id);
}