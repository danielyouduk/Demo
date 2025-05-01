using Services.Core.Entities;

namespace DriverService.Domain.Entities;

public class ReportEntity : BaseEntity
{
    public int AccountId { get; init; }

    public int DriverId { get; init; }

    public int VehicleId { get; init; }
    
    public string? BlobUrl { get; init; }
    
    public string? ContentType { get; init; }
    
    public int FileSize { get; init; }
}