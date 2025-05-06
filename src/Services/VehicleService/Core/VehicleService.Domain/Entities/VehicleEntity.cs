using Services.Core.Entities;

namespace VehicleService.Domain.Entities;

public class VehicleEntity : BaseEntity
{
    public required Guid AccountId { get; init; }

    public required string RegistrationNumber { get; init; }
}