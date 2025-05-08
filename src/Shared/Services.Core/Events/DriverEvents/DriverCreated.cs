namespace Services.Core.Events.DriverEvents;

public record DriverCreated(
    Guid DriverId,
    Guid AccountId,
    string FirstName,
    string LastName,
    string ResourceUrl,
    DateTime CreatedAt,
    DateTime UpdatedAt);