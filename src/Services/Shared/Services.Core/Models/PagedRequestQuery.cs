namespace Services.Core.Models;

public class PagedRequestQuery
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SortBy { get; init; }
    public bool SortDescending { get; init; }
    public bool? IsActive { get; init; }
}