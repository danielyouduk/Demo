using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;

public record GetVehiclesQuery(
    PagedRequestQuery PagedRequestQuery) : IRequest<ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>>;