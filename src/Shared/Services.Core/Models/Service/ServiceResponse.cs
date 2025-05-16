using Services.Core.Enums;

namespace Services.Core.Models.Service;

public class ServiceResponse<T>
{
    public required ServiceStatus Status { get; set; }
    
    public required string Message { get; set; }
    
    public T? Data { get; set; }
}