namespace mini_shop_backend;

public class Cart : BaseEntity
{
    public Guid UserId { get; set; }

    public List<CartItem> Items { get; set; }
}