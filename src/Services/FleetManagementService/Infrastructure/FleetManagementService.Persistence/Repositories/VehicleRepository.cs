using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;
using FleetManagementService.Application.Features.Vehicle.Shared;
using FleetManagementService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class VehicleRepository(
    FleetManagementDatabaseContext context,
    IMapper mapper,
    ILogger<VehicleRepository> logger) : IVehicleRepository
{
    public async Task<BasePagedResult<VehicleDto>> GetVehiclesAsync(PagedRequestQuery pagedRequestQuery,
        CancellationToken cancellationToken)
    {
        try
        {
            // Base query
            var query = context.Vehicles.AsNoTracking().AsQueryable();
        
            // Apply sorting
            if (!string.IsNullOrEmpty(pagedRequestQuery.SortBy))
            {
                query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
            }
        
            // Execute the query with pagination
            var queryCount = await query.CountAsync(cancellationToken);
            
            if (queryCount == 0)
            {
                return new BasePagedResult<VehicleDto>
                {
                    Data = Array.Empty<VehicleDto>(),
                    TotalRecords = 0
                };
            }
        
            // Apply pagination
            var vehicles = await query.
                ApplyPaging(pagedRequestQuery)
                .ToListAsync(cancellationToken);
        
            // Map and return the final result
            var mappedVehicles = mapper.Map<IReadOnlyCollection<VehicleDto>>(vehicles);
            
            return new BasePagedResult<VehicleDto>
            {
                Data = mappedVehicles,
                TotalRecords = queryCount
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehicleRepository.GetVehiclesAsync
            logger.LogError(e, string.Empty, pagedRequestQuery);
            throw;
        }
    }

    public async Task<VehicleDto?> GetVehicleByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            // todo: Add ArgumentException message for DriverRepository.GetDriverByIdAsync
            throw new ArgumentException(string.Empty, nameof(id));
        }

        try
        {
            var vehicle = await context.Vehicles.AsNoTracking()
                .FirstOrDefaultAsync(vehicle => vehicle.Id == id, cancellationToken);

            return vehicle != null ? mapper.Map<VehicleDto>(vehicle) : null;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehicleRepository.GetVehicleByIdAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }

    public async Task<VehicleDto> CreateAsync(CreateVehicleCommand createVehicleCommand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(createVehicleCommand);
        
        try
        {
            var entity = mapper.Map<VehicleDto>(createVehicleCommand);
        
            await context.AddAsync(entity, cancellationToken);
        
            return mapper.Map<VehicleDto>(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehicleRepository.CreateAsync
            logger.LogError(e, string.Empty, createVehicleCommand.RegistrationNumber);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Vehicles
                .AsNoTracking()
                .AnyAsync(vehicle => vehicle.Id == id, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for VehicleRepository.ExistsAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }
}