namespace mini_shop_backend;

public class Category : BaseEntity
{
    public string Name { get; set; }

    public List<Product> Products { get; set; }
}