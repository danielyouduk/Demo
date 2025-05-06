using Services.Core.Entities;

namespace FleetManagementService.Domain.Entities;

public class DriverEntity : BaseEntity
{
    public Guid AccountId { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public Account? Account { get; init; }
}