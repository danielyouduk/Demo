using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;

public record GetVehicleQuery(Guid Id)
    : IRequest<ServiceResponse<VehicleDto>>;