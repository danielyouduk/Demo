namespace Services.Core.Models.Service;

public class ServiceResponse<T>
{
    public required bool Success { get; set; }
    
    public required string Message { get; set; }
    
    public T? Data { get; set; }
}