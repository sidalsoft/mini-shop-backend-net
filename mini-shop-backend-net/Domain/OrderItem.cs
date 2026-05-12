namespace miniShopBackendNet.Domain;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; }

    public Guid ProductId { get; set; }

    public string ProductName { get; set; }
    public decimal Price { get; set; }

    public int Quantity { get; set; }
}