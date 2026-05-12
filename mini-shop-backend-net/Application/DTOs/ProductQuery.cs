namespace miniShopBackendNet.Application.DTOs;

public class ProductQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CategoryId { get; set; }

    public string? Name { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; } = "asc"; // asc / desc
}