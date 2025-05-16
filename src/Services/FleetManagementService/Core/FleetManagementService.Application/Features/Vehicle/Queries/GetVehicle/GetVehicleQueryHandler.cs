using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;

public class GetVehicleQueryHandler(IVehicleRepository vehicleRepository) 
    : IRequestHandler<GetVehicleQuery, ServiceResponse<VehicleDto>>
{
    public async Task<ServiceResponse<VehicleDto>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        var vehicle = await vehicleRepository.GetVehicleByIdAsync(request.Id);

        return new ServiceResponse<VehicleDto>
        {
            Data = vehicle,
            Status = ServiceStatus.Success,
            Message = "Success"
        };
    }
}