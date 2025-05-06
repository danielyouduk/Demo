namespace Services.Core.Models;

public class BasePagedResult<T>
{
    public IReadOnlyCollection<T>? Data { get; init; }
    
    public int TotalRecords { get; init; }
}