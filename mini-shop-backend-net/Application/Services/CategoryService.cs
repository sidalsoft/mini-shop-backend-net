using Microsoft.EntityFrameworkCore;
using mini_shop_backend_net.Application.Common.Exceptions;
using mini_shop_backend_net.Application.DTOs.Category;
using mini_shop_backend_net.Infrastructure.Repositories;
using mini_shop_backend_net;
using mini_shop_backend_net.Domain;

namespace mini_shop_backend_net.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoryDto>> GetAll()
    {
        return await _repo.Query()
            .Select(c => new CategoryDto(c.Id,  c.Name))
            .ToListAsync();
    }

    public async Task<CategoryDto?> GetById(Guid id)
    {
        var c = await _repo.GetByIdAsync(id);
        if (c == null) return null;

        return new CategoryDto(c.Id,  c.Name);
    }

    public async Task Create(CreateCategoryDto dto)
    {
        var exists = await _repo.ExistsByName(dto.Name);
        if (exists)
            throw new AppException("Категория уже существует");

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            CreatedAt = DateTime.UtcNow
        };

        await _repo.AddAsync(category);
        await _repo.SaveChangesAsync();
    }

    public async Task Update(Guid id, CreateCategoryDto dto)
    {
        
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException("Категория не найдена");
        if (category.DeletedAt != null)
            throw new AppException("Категория удалена");

        category.Name = dto.Name;
        category.UpdatedAt = DateTime.UtcNow;

        _repo.Update(category);
        await _repo.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var category = await _repo.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException("Категория не найдена");
        if (category.DeletedAt != null)
            throw new AppException("Категория удалена");

        var hasProducts = await _repo.HasProducts(id);
        if (hasProducts)
            throw new AppException("Нельзя удалить категорию с товарами");

        category.DeletedAt = DateTime.UtcNow;

        _repo.Update(category);
        await _repo.SaveChangesAsync();
    }
}