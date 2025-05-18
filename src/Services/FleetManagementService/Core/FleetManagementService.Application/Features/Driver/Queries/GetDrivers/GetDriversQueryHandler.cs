using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDrivers;

public class GetDriversQueryHandler(
    IDriverRepository driverRepository,
    GetDriversQueryValidator validator,
    ILogger<GetDriversQueryHandler> logger) 
    : IRequestHandler<GetDriversQuery, ServiceResponseCollection<IReadOnlyCollection<DriverDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<DriverDto>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var drivers = await driverRepository.GetDriversAsync(request.PagedRequestQuery, cancellationToken);

            return new ServiceResponseCollection<IReadOnlyCollection<DriverDto>>
            {
                Data = drivers.Data,
                TotalRecords = drivers.TotalRecords,
                PageSize = request.PagedRequestQuery.PageSize,
                Message = "Success",
                Status = ServiceStatus.Success,
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetDriversQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}