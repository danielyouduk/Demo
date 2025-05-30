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
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponseCollection<IReadOnlyCollection<DriverDto>>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
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
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
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