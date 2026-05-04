namespace mini_shop_backend;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; } // snapshot
    public decimal Price { get; set; }

    public int Quantity { get; set; }
}