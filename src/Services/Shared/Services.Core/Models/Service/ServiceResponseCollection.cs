namespace Services.Core.Models.Service;

public class ServiceResponseCollection<T> : ServiceResponse<T>
{
    private readonly int _pageSize = 1;
    private readonly int _totalRecords;

    public int TotalRecords 
    { 
        get => _totalRecords;
        init => _totalRecords = Math.Max(0, value);
    }
    
    public int PageSize 
    { 
        get => _pageSize;
        init => _pageSize = value > 0 ? value : 1;
    }
    
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
}
