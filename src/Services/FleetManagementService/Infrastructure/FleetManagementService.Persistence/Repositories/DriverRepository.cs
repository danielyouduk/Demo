using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Commands.UpdateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using FleetManagementService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class DriverRepository(
    FleetManagementDatabaseContext context,
    ILogger<DriverRepository> logger,
    IMapper mapper) : IDriverRepository
{
    public async Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken)
    {
        try
        {
            // Base query
            var query = context.Drivers.AsQueryable();
        
            // Apply sorting
            if (!string.IsNullOrEmpty(pagedRequestQuery.SortBy))
            {
                query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
            }

            // Execute the query with pagination
            var queryCount = await query.CountAsync(cancellationToken);
            
            if (queryCount == 0)
            {
                return new BasePagedResult<DriverDto>
                {
                    Data = Array.Empty<DriverDto>(),
                    TotalRecords = 0
                };
            }
        
            // Apply pagination
            var drivers = await query
                .ApplyPaging(pagedRequestQuery)
                .ToListAsync(cancellationToken);
            
            // Map and return the final result
            var mappedDrivers = mapper.Map<IReadOnlyCollection<DriverDto>>(drivers);
            return new BasePagedResult<DriverDto>
            {
                Data = mappedDrivers,
                TotalRecords = queryCount
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverRepository.GetDriversAsync
            logger.LogError(e, string.Empty, pagedRequestQuery);
            throw;
        }
    }

    public async Task<DriverDto?> GetDriverByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        if (id == Guid.Empty)
        {
            // todo: Add ArgumentException message for DriverRepository.GetDriverByIdAsync
            throw new ArgumentException(string.Empty, nameof(id));
        }

        try
        {
            var driver = await context.Drivers.AsNoTracking()
                .FirstOrDefaultAsync(driver => driver.Id == id, cancellationToken);

            return driver != null ? mapper.Map<DriverDto>(driver) : null;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverRepository.GetDriverByIdAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }
    
    public async Task<DriverDto> CreateAsync(CreateDriverCommand createDriverCommand, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(createDriverCommand);

        try
        {
            var entity = mapper.Map<Driver>(createDriverCommand);
        
            await context.AddAsync(entity, cancellationToken);
        
            return mapper.Map<DriverDto>(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverRepository.CreateAsync
            logger.LogError(e, string.Empty, createDriverCommand.FirstName);
            throw;
        }
    }

    public async Task<bool> UpdateDriverAsync(UpdateDriverCommand command, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        try
        {
            var existingDriver = await context.Drivers
                .FirstOrDefaultAsync(a => a.Id == command.Id, cancellationToken);
            
            if (existingDriver == null)
            {
                return false;
            }

            mapper.Map(command, existingDriver);
            
            context.Entry(existingDriver).Property(p => p.CreatedAt).IsModified = false;
            context.Update(existingDriver);

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverRepository.UpdateDriver
            logger.LogError(e, string.Empty, command.FirstName);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Drivers
                .AsNoTracking()
                .AnyAsync(driver => driver.Id == id, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for DriverRepository.ExistsAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }
}