using miniShopBackendNet.Application.DTOs;
using miniShopBackendNet.Application.DTOs.Order;

namespace miniShopBackendNet.Application.Services;

public interface IOrderService
{
    Task<Guid> CreateOrder(Guid userId, CreateOrderDto dto);

    Task<List<OrderDto>> GetUserOrders(Guid userId);

    Task<PagedResult<OrderDto>> GetAllOrders(OrderQuery query);
}