namespace DriverService.Application.Features.Drivers.Events;

public record DriverCreated
{
    public Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public DateTime CreatedAt { get; init; }
}