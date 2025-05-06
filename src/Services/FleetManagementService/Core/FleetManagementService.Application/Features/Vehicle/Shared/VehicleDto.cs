namespace FleetManagementService.Application.Features.Vehicle.Shared;

public class VehicleDto
{
    public Guid Id { get; set; }
    
    public required string RegistrationNumber { get; init; }
}