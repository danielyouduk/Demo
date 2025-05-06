using DriverService.Application.Features.Drivers.Shared;
using Services.Core;
using Services.Core.Models;

namespace DriverService.Application.Contracts.Persistence;

public interface IDriverQueryRepository
{
    Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<DriverDto> GetDriverByIdAsync(Guid id);
}