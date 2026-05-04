using mini_shop_backend_net.Application.Common;
using mini_shop_backend_net.Application.Common.Exceptions;
using mini_shop_backend_net.Application.DTOs;
using mini_shop_backend_net.Infrastructure;
using mini_shop_backend_net.Infrastructure.Repositories;
using mini_shop_backend_net.Infrastructure.Repositories.Repositories;
using mini_shop_backend;

namespace mini_shop_backend_net.Application.Services;

using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly ICategoryRepository _categoryRepo;


    public ProductService(IProductRepository repo, ICategoryRepository categoryRepo)
    {
        _repo = repo;
        _categoryRepo = categoryRepo;
    }

    public async Task<PagedResult<ProductDto>> GetAll(ProductQuery query)
    {
        // ✅ нормализация
        query.Page = Math.Max(query.Page, 1);
        query.PageSize = Math.Min(Math.Max(query.PageSize, 1), 50);

        var dbQuery = _repo.Query().AsQueryable();

        // 🔍 Filtering
        if (!string.IsNullOrEmpty(query.Name))
        {
            dbQuery = dbQuery.Where(p =>
                p.Name.ToLower().Contains(query.Name.ToLower()));
        }

        if (query.MinPrice.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.Price >= query.MinPrice.Value);
        }

        if (query.MaxPrice.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.Price <= query.MaxPrice.Value);
        }
        
        if (query.CategoryId.HasValue)
        {
            dbQuery = dbQuery.Where(p => p.CategoryId == query.CategoryId.Value);
        }

        // 🔄 Sorting
        dbQuery = ApplySorting(dbQuery, query);

        var totalCount = await dbQuery.CountAsync();

        // 📄 Pagination
        var items = await dbQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name
            })
            .ToListAsync();

        return new PagedResult<ProductDto>
        {
            Content = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<ProductDto> GetById(Guid id)
    {
        var p = await _repo.GetByIdAsync(id);

        if (p == null) return null;

        return new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            CategoryId = p.CategoryId,
            CategoryName = p.Category.Name
        };
    }

    public async Task Create(CreateProductDto dto)
    {
        var categoryExists = await _categoryRepo.Exists(dto.CategoryId);

        if (!categoryExists)
            throw new AppException("Категория не найдена", 404);

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            CreatedAt = DateTime.UtcNow,
            CategoryId = dto.CategoryId
        };

        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync();
    }

    public async Task Update(Guid id, CreateProductDto dto)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return;

        product.Name = dto.Name;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.ImageUrl = dto.ImageUrl;
        product.CategoryId = dto.CategoryId;

        _repo.Update(product);
        await _repo.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var product = await _repo.GetByIdAsync(id);
        if (product == null) return;

        _repo.Delete(product);
        await _repo.SaveChangesAsync();
    }

    private IQueryable<Product> ApplySorting(IQueryable<Product> query, ProductQuery request)
    {
        var isDesc = request.SortDirection?.ToLower() == "desc";

        return request.SortBy?.ToLower() switch
        {
            "name" => isDesc
                ? query.OrderByDescending(p => p.Name)
                : query.OrderBy(p => p.Name),

            "price" => isDesc
                ? query.OrderByDescending(p => p.Price)
                : query.OrderBy(p => p.Price),

            "createdat" => isDesc
                ? query.OrderByDescending(p => p.CreatedAt)
                : query.OrderBy(p => p.CreatedAt),

            _ => query.OrderByDescending(p => p.CreatedAt) // default
        };
    }
}