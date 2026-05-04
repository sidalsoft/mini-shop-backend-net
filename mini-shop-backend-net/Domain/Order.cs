namespace mini_shop_backend;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }

    public string Name { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderStatus Status { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}