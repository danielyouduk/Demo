using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Vehicle.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;

public class GetVehicleQueryHandler(
    IVehicleRepository vehicleRepository,
    GetVehicleQueryValidator validator,
    ILogger<GetVehicleQueryHandler> logger) 
    : IRequestHandler<GetVehicleQuery, ServiceResponse<VehicleDto>>
{
    public async Task<ServiceResponse<VehicleDto>> Handle(GetVehicleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<VehicleDto>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var vehicle = await vehicleRepository.GetVehicleByIdAsync(request.Id, cancellationToken);
            
            if (vehicle == null)
            {
                return new ServiceResponse<VehicleDto>
                {
                    Status = ServiceStatus.NotFound,
                    Message = $"Vehicle with ID {request.Id} not found",
                    Data = null
                };
            }

            return new ServiceResponse<VehicleDto>
            {
                Data = vehicle,
                Status = ServiceStatus.Success,
                Message = "Success"
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetVehicleQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}