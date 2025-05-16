using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDrivers;

public class GetDriversQueryHandler(IDriverRepository driverRepository) 
    : IRequestHandler<GetDriversQuery, ServiceResponseCollection<IReadOnlyCollection<DriverDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<DriverDto>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
    {
        var drivers = await driverRepository.GetDriversAsync(request.PagedRequestQuery);

        return new ServiceResponseCollection<IReadOnlyCollection<DriverDto>>
        {
            Data = drivers.Data,
            TotalRecords = drivers.TotalRecords,
            PageSize = request.PagedRequestQuery.PageSize,
            Message = "Success",
            Status = ServiceStatus.Success,
        };
    }
}