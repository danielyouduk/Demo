using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDrivers;

public record GetDriversQuery(
    PagedRequestQuery PagedRequestQuery) : IRequest<ServiceResponseCollection<IReadOnlyCollection<DriverDto>>>;