using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;

public class CreateVehicleCommandHandler(
    CreateVehicleCommandValidator validator,
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<CreateVehicleCommandHandler> logger) : IRequestHandler<CreateVehicleCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ServiceResponse<Guid>
            {
                Status = ServiceStatus.Success,
                Message = validationResult.Errors.First().ErrorMessage
            };

        try
        {
            var vehicle = await vehicleRepository.CreateAsync(request, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            return new ServiceResponse<Guid>
            {
                Data = vehicle.Id,
                Status = ServiceStatus.Success,
                Message = $"Successfully created vehicle {vehicle}"
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for CreateDriverCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}