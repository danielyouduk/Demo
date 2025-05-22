using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;

public class UpdateDriverCommandHandler(
    IDriverRepository driverRepository,
    UpdateDriverCommandValidator validator,
    ILogger<UpdateDriverCommandHandler> logger,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateDriverCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<Unit>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var updated = await driverRepository.UpdateDriverAsync(request, cancellationToken);
            if (!updated)
            {
                return new ServiceResponse<Unit>
                {
                    Status = ServiceStatus.NotFound,
                    Message = "Driver not found"
                };
            }
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Success,
                Message = "Driver updated successfully"
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for UpdateDriverCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}