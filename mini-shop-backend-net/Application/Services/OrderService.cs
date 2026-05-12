using Microsoft.EntityFrameworkCore;
using miniShopBackendNet.Application.Common.Exceptions;
using miniShopBackendNet.Application.DTOs;
using miniShopBackendNet.Application.DTOs.Order;
using miniShopBackendNet.Infrastructure;
using miniShopBackendNet.Infrastructure.Repositories;
using miniShopBackendNet.Domain;

namespace miniShopBackendNet.Application.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;
    private readonly ICartRepository _cartRepo;

    public OrderService(AppDbContext context, ICartRepository cartRepo)
    {
        _context = context;
        _cartRepo = cartRepo;
    }

    // -------------------- CREATE ORDER --------------------
    public async Task<Guid> CreateOrder(Guid userId, CreateOrderDto dto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        var cart = await _cartRepo.GetOrCreateAsync(userId);

        if (!cart.Items.Any())
            throw new AppException("Корзина пустая");

        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = dto.Name,
            Phone = dto.Phone,
            Address = dto.Address,
            Status = OrderStatus.Pending,
            Items = new List<OrderItem>()
        };

        decimal total = 0;

        foreach (var cartItem in cart.Items)
        {
            var product = cartItem.Product;

            if (product == null || product.DeletedAt != null)
                throw new AppException("Товар недоступен");

            var itemTotal = product.Price * cartItem.Quantity;

            order.Items.Add(new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = cartItem.Quantity
            });

            total += itemTotal;
        }

        order.TotalAmount = total;

        await _context.Orders.AddAsync(order);

        cart.Items.Clear();

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return order.Id;
    }

    // -------------------- USER ORDERS --------------------
    public async Task<List<OrderDto>> GetUserOrders(Guid userId)
    {
        return await _context.Orders
            .Where(x => x.UserId == userId)
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                Name = o.Name,
                Phone = o.Phone,
                Address = o.Address,
                TotalPrice = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto(
                    i.ProductId,
                    i.Quantity,
                    i.ProductName,
                    i.Price
                )).ToList()
            })
            .ToListAsync();
    }

    // -------------------- ADMIN --------------------
    public async Task<PagedResult<OrderDto>> GetAllOrders(OrderQuery query)
    {
        query.Page = Math.Max(query.Page, 1);
        query.PageSize = Math.Min(Math.Max(query.PageSize, 1), 50);

        var dbQuery = _context.Orders
            .Include(o => o.Items)
            .AsQueryable();

        if (query.UserId.HasValue)
            dbQuery = dbQuery.Where(x => x.UserId == query.UserId.Value);

        if (query.From.HasValue)
            dbQuery = dbQuery.Where(x => x.CreatedAt >= query.From.Value);

        if (query.To.HasValue)
            dbQuery = dbQuery.Where(x => x.CreatedAt <= query.To.Value);

        dbQuery = query.SortDirection == "asc"
            ? dbQuery.OrderBy(x => x.CreatedAt)
            : dbQuery.OrderByDescending(x => x.CreatedAt);

        var totalCount = await dbQuery.CountAsync();

        var items = await dbQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(o => new OrderDto
            {
                Id = o.Id,
                UserId = o.UserId,
                Name = o.Name,
                Phone = o.Phone,
                Address = o.Address,
                TotalPrice = o.TotalAmount,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt,
                Items = o.Items.Select(i => new OrderItemDto(
                    i.ProductId,
                    i.Quantity,
                    i.ProductName,
                    i.Price
                )).ToList()
            })
            .ToListAsync();

        return new PagedResult<OrderDto>
        {
            Content = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }
}