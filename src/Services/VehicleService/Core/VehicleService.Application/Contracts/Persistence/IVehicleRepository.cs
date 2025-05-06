using Services.Core.Models;
using VehicleService.Application.Features.Vehicle.Shared;

namespace VehicleService.Application.Contracts.Persistence;

public interface IVehicleRepository
{
    Task<BasePagedResult<VehicleDto>> GetVehiclesAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<VehicleDto> GetVehicleByIdAsync(Guid id);
}