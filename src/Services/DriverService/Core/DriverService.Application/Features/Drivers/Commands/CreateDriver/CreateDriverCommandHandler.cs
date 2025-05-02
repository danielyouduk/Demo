using AutoMapper;
using DriverService.Application.Contracts.Persistence;
using DriverService.Domain.Entities;
using MassTransit;
using MediatR;
using Services.Core.Events.DriverEvents;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Commands.CreateDriver;

public class CreateDriverCommandHandler(
    IMapper mapper,
    IDriverCommandRepository driverCommandRepository,
    IPublishEndpoint publishEndpoint) : IRequestHandler<CreateDriverCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        var driver = await driverCommandRepository.CreateDriver(mapper.Map<DriverEntity>(request));

        await publishEndpoint.Publish(new DriverCreated(driver.Id, driver.FirstName, driver.LastName, driver.CreatedAt), cancellationToken);
        
        return new ServiceResponse<Guid>
        {
            Data = driver.Id,
            Success = true,
            Message = "Successfully created driver."
        };
    }
}