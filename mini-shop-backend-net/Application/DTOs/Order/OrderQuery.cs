namespace miniShopBackendNet.Application.DTOs.Order;

public class OrderQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public Guid? UserId { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public string? SortDirection { get; set; } = "desc";
}