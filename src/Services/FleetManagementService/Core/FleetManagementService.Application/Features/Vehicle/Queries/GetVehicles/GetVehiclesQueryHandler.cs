using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;

public class GetVehiclesQueryHandler(
    IVehicleRepository vehicleRepository,
    GetVehiclesQueryValidator validator,
    ILogger<GetVehiclesQueryHandler> logger) 
    : IRequestHandler<GetVehiclesQuery, ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var vehicles = await vehicleRepository.GetVehiclesAsync(request.PagedRequestQuery, cancellationToken);

            return new ServiceResponseCollection<IReadOnlyCollection<VehicleDto>>
            {
                Data = vehicles.Data,
                TotalRecords = vehicles.TotalRecords,
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
            // todo: Add Exception log message for GetVehiclesQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}