using FleetManagementService.Application.Features.Vehicle.Shared;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IVehicleRepository
{
    Task<BasePagedResult<VehicleDto>> GetVehiclesAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<VehicleDto> GetVehicleByIdAsync(Guid id);
}