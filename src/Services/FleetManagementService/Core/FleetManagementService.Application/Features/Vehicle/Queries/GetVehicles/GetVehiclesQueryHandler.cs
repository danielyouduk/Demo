using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;

public class GetVehiclesQueryHandler(IVehicleRepository vehicleRepository) 
    : IRequestHandler<GetVehiclesQuery, ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        var vehicles = await vehicleRepository.GetVehiclesAsync(request.PagedRequestQuery);

        return new ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>
        {
            Data = vehicles.Data,
            TotalRecords = vehicles.TotalRecords,
            PageSize = request.PagedRequestQuery.PageSize,
            Message = "Success",
            Status = ServiceStatus.Success,
        };
    }
}