using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MassTransit;
using MediatR;
using Services.Core.Events.DriverEvents;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandHandler(
    CreateDriverCommandValidator validator,
    IDriverRepository driverCommandRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateDriverCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = validationResult.Errors.First().ErrorMessage
            };

        try
        {
            var driver = await driverCommandRepository.CreateAsync(request);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
            
            await publishEndpoint.Publish(mapper.Map<DriverCreated>(driver), cancellationToken);
            
            return new ServiceResponse<Guid>
            {
                Data = driver.Id,
                Success = true,
                Message = $"Successfully created driver {driver.FirstName} {driver.LastName}."
            };
        }
        catch (Exception e)
        {
            return new ServiceResponse<Guid>
            {
                Success = false,
                Message = $"An error occurred while creating the driver {request.FirstName} {request.LastName}."
            };
        }
    }
}