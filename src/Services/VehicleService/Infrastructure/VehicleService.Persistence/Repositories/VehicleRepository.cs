using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;
using VehicleService.Application.Contracts.Persistence;
using VehicleService.Application.Features.Vehicle.Shared;
using VehicleService.Persistence.DatabaseContext;

namespace VehicleService.Persistence.Repositories;

public class VehicleRepository(VehicleDatabaseContext context, IMapper mapper) : IVehicleRepository
{
    public async Task<BasePagedResult<VehicleDto>> GetVehiclesAsync(PagedRequestQuery pagedRequestQuery)
    {
        // Base query
        var query = context.Vehicles
            .AsQueryable();
        
        // Apply sorting
        query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
        
        // Execute the query with pagination
        var queryCount = await query.CountAsync();
        
        // Apply pagination
        var vehicles = await query.ApplyPaging(pagedRequestQuery)
            .ToListAsync();
        
        // Map and return the final result
        return new BasePagedResult<VehicleDto>
        {
            Data = mapper.Map<IReadOnlyCollection<VehicleDto>>(vehicles),
            TotalRecords = queryCount
        };
    }

    public async Task<VehicleDto> GetVehicleByIdAsync(Guid id)
    {
        var vehicle = await context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);

        return mapper.Map<VehicleDto>(vehicle);
    }
}