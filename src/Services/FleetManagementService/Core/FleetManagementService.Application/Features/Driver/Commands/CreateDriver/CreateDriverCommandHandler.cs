using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Services.Core.Events.DriverEvents;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandHandler(
    IMapper mapper,
    IDriverRepository driverCommandRepository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateDriverCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverCommandRepository.CreateDriver(mapper.Map<Domain.Entities.Driver>(request));

        await publishEndpoint.Publish(new DriverCreated(driver.Id, driver.FirstName, driver.LastName, driver.CreatedAt), cancellationToken);
        
        return new ServiceResponse<Guid>
        {
            Data = driver.Id,
            Success = true,
            Message = "Successfully created driver."
        };
    }
}