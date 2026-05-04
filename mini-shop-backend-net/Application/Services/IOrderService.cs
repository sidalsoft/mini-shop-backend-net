using mini_shop_backend_net.Application.DTOs;
using mini_shop_backend_net.Application.DTOs.Order;

namespace mini_shop_backend_net.Application.Services;

public interface IOrderService
{
    Task<Guid> CreateOrder(Guid userId, CreateOrderDto dto);

    Task<List<OrderDto>> GetUserOrders(Guid userId);

    Task<PagedResult<OrderDto>> GetAllOrders(OrderQuery query);
}