using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IDriverRepository
{
    Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<DriverDto> GetDriverByIdAsync(Guid id);
    
    Task<DriverDto> CreateAsync(CreateDriverCommand driver);
    
    Task UpdateDriver(object driver);
}