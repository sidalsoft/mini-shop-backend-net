namespace mini_shop_backend_net.Application.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; }
    
    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

}