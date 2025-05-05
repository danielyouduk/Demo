using DriverService.Application.Contracts.Persistence;
using DriverService.Application.Features.Drivers.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace DriverService.Application.Features.Drivers.Queries.GetDriver;

public class GetDriverQueryHandler(IDriverQueryRepository driverRepository) 
    : IRequestHandler<GetDriverQuery, ServiceResponse<DriverDto>>
{
    public async Task<ServiceResponse<DriverDto>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
    {
        var driverEntity = await driverRepository.GetDriverByIdAsync(request.Id);

        return new ServiceResponse<DriverDto>
        {
            Data = driverEntity,
            Success = true,
            Message = "Success"
        };
    }
}