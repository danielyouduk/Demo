namespace FleetManagementService.Application.Features.Driver.Shared;

public class DriverDto
{
    public Guid Id { get; init; }
    
    public Guid AccountId { get; init; }
    
    public string FirstName { get; init; } = string.Empty;
    
    public string LastName { get; init; } = string.Empty;
    
    public bool IsActive { get; init; }
    
    public DateTime CreatedAt { get; init; }
    
    public DateTime UpdatedAt { get; init; }
}