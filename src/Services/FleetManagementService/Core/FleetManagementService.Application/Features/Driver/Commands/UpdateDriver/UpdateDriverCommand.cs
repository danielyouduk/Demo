namespace FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;

public record UpdateDriverCommand
{
    public required string Name { get; init; }
}