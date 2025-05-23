using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDriver;

public class GetDriverQueryHandler(
    IDriverRepository driverRepository,
    GetDriverQueryValidator validator,
    ILogger<GetDriverQueryHandler> logger) 
    : IRequestHandler<GetDriverQuery, ServiceResponse<DriverDto>>
{
    public async Task<ServiceResponse<DriverDto>> Handle(
        GetDriverQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<DriverDto>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var driver = await driverRepository.GetDriverByIdAsync(request.Id, cancellationToken);
            
            if (driver == null)
            {
                return new ServiceResponse<DriverDto>
                {
                    Status = ServiceStatus.NotFound,
                    Message = $"Driver with ID {request.Id} not found",
                    Data = null
                };
            }
            
            return new ServiceResponse<DriverDto>
            {
                Data = driver,
                Status = ServiceStatus.Success,
                Message = "Success"
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetDriverQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}