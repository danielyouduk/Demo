namespace Services.Core.Events.DriverEvents;

public record DriverCreated(Guid DriverId, string FirstName, string LastName, DateTime CreatedAt);