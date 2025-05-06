using Services.Core.Entities;

namespace FleetManagementService.Domain.Entities;

public class VehicleEntity : BaseEntity
{
    public required Guid AccountId { get; init; }

    public required string RegistrationNumber { get; init; }
    
    public Account? Account { get; init; }
}