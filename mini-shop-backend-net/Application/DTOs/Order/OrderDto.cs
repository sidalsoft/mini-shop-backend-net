namespace mini_shop_backend_net.Application.DTOs.Order;

public record OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public decimal TotalPrice { get; set; }
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public List<OrderItemDto> Items { get; set; }
}