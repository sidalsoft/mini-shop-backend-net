namespace mini_shop_backend_net.Application.DTOs;

public record CreateProductDto
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string ImageUrl { get; init; }
    public Guid CategoryId { get; init; }
}