using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDriver;

public class GetDriverQueryHandler(IDriverRepository driverRepository) 
    : IRequestHandler<GetDriverQuery, ServiceResponse<DriverDto>>
{
    public async Task<ServiceResponse<DriverDto>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driver = await driverRepository.GetDriverByIdAsync(request.Id);

        return new ServiceResponse<DriverDto>
        {
            Data = driver,
            Success = true,
            Message = "Success"
        };
    }
}