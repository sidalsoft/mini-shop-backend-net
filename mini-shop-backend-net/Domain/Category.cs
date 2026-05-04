namespace mini_shop_backend_net.Domain;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public List<Product> Products { get; set; }
}