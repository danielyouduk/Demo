using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public record CreateDriverCommand(
    Guid AccountId,
    string FirstName,
    string LastName) : IRequest<ServiceResponse<Guid>>;