using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Commands.CreateDriver;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using FleetManagementService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class DriverRepository(FleetManagementDatabaseContext context, IMapper mapper) : IDriverRepository
{
    public async Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery pagedRequestQuery)
    {
        // Base query
        var query = context.Drivers
            .AsQueryable();
        
        // Apply sorting
        query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
        
        // Execute the query with pagination
        var queryCount = await query.CountAsync();
        
        // Apply pagination
        var drivers = await query.ApplyPaging(pagedRequestQuery)
            .ToListAsync();
        
        // Map and return the final result
        return new BasePagedResult<DriverDto>
        {
            Data = mapper.Map<IReadOnlyCollection<DriverDto>>(drivers),
            TotalRecords = queryCount
        };
    }

    public async Task<DriverDto> GetDriverByIdAsync(Guid id)
    {
        var driver = await context.Drivers.FirstOrDefaultAsync(driver => driver.Id == id);

        return mapper.Map<DriverDto>(driver);
    }
    
    public async Task<DriverDto> CreateAsync(CreateDriverCommand createDriverCommand)
    {
        var entity = mapper.Map<Driver>(createDriverCommand);
        
        var now = DateTime.UtcNow;
        entity.CreatedAt = now;
        entity.UpdatedAt = now;
        entity.IsActive = true;
        
        await context.AddAsync(entity);
        
        return mapper.Map<DriverDto>(entity);
    }

    public Task UpdateDriver(object driver)
    {
        throw new NotImplementedException();
    }
}