using FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;
using FleetManagementService.Application.Features.Vehicle.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IVehicleRepository
{
    Task<BasePagedResult<VehicleDto>> GetVehiclesAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken);
    
    Task<VehicleDto?> GetVehicleByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<VehicleDto> CreateAsync(CreateVehicleCommand vehicle, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}