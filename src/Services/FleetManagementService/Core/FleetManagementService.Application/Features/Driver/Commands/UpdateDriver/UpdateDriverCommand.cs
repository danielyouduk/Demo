using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;

public record UpdateDriverCommand(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName) : IRequest<ServiceResponse<Unit>>;