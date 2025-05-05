using Services.Core.Entities;

namespace DriverService.Domain.Entities;

public class DriverEntity : BaseEntity
{
    public Guid AccountId { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
}