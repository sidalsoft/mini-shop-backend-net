namespace miniShopBackendNet.Application.DTOs.Order;

public record CreateOrderDto(
    string Name,
    string Phone,
    string Address
);