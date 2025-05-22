using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Events.DriverEvents;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandHandler(
    CreateDriverCommandValidator validator,
    IDriverRepository driverCommandRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILogger<CreateDriverCommandHandler> logger,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateDriverCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
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
            var driver = await driverCommandRepository.CreateAsync(request, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            await publishEndpoint.Publish(mapper.Map<DriverCreated>(driver), cancellationToken);
            
            return new ServiceResponse<Guid>
            {
                Data = driver.Id,
                Status = ServiceStatus.Success,
                Message = $"Successfully created driver {driver.FirstName} {driver.LastName}."
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