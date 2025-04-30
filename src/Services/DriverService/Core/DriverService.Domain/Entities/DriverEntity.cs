using DriverService.Domain.Entities.Common;

namespace DriverService.Domain.Entities;

public class DriverEntity : BaseEntity
{
    public required string Name { get; set; }
}