namespace mini_shop_backend_net.Application.DTOs.Order;

public record CreateOrderDto(
    string Name,
    string Phone,
    string Address
);