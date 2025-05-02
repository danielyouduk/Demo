namespace DriverService.Application.Features.Drivers.Events;

public record DriverCreated(Guid DriverId, string FirstName, string LastName, DateTime CreatedAt);