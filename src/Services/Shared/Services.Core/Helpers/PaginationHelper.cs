using Services.Core.Models;

namespace Services.Core.Helpers;

public static class PaginationHelper
{
    public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, PagedRequestQuery paginationParameters)
    {
        var pageNumber = paginationParameters.PageNumber <= 0 ? 1 : paginationParameters.PageNumber;

        return query
            .Skip((pageNumber - 1) * paginationParameters.PageSize)
            .Take(paginationParameters.PageSize);
    }
    
    public static PagedRequestQuery ToPagedRequest(this PagedRequestQuery parameters) =>
        new()
        {
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize,
            SortBy = parameters.SortBy,
            SortDescending = parameters.SortDescending,
            IsActive = parameters.IsActive
        };
}
