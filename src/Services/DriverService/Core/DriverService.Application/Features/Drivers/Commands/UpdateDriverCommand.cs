namespace DriverService.Application.Features.Drivers.Commands;

public record UpdateDriverCommand
{
    public required string Name { get; init; }
}