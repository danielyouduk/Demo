using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IDriverRepository
{
    Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken);
    
    Task<DriverDto?> GetDriverByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<DriverDto> CreateAsync(CreateDriverCommand driver, CancellationToken cancellationToken);
    
    Task<bool> UpdateDriverAsync(UpdateDriverCommand command, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}