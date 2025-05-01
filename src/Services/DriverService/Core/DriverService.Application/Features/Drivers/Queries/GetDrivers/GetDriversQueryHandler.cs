using DriverService.Application.Contracts.Persistence;
using DriverService.Application.Features.Drivers.Shared;
using MediatR;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Queries.GetDrivers;

public class GetDriversQueryHandler(IDriverQueryRepository driverRepository) 
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
            Success = true
        };
    }
}