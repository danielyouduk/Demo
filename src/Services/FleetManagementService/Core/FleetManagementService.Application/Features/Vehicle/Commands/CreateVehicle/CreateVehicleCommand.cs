using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;

public record CreateVehicleCommand(
    Guid AccountId,
    string RegistrationNumber) : IRequest<ServiceResponse<Guid>>;