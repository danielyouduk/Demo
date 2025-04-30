using DriverService.Domain.Entities.Common;

namespace DriverService.Domain.Entities;

public class ReportEntity : BaseEntity
{
    public int AccountId { get; init; }

    public int DriverId { get; init; }

    public int VehicleId { get; init; }
}