namespace mini_shop_backend_net.Application.DTOs;

public class PagedResult<T>
{
    public List<T> Content { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}