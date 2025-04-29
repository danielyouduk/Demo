namespace DriverService.Application.Features.Drivers.Commands;

public record CreateDriverCommand
{
    public required string Name { get; init; }
}