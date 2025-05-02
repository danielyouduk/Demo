namespace DriverService.Application.Features.Drivers.Commands.UpdateDriver;

public record UpdateDriverCommand
{
    public required string Name { get; init; }
}