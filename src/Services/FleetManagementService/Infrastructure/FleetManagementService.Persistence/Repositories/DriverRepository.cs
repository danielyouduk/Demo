using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Driver.Shared;
using FleetManagementService.Domain.Entities;
using FleetManagementService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class DriverRepository(AccountDatabaseContext context, IMapper mapper) : IDriverRepository
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
    
    public async Task<DriverDto> CreateDriver(Driver driver)
    {
        await context.AddAsync(driver);
        await context.SaveChangesAsync();

        return mapper.Map<DriverDto>(driver);
    }

    public Task UpdateDriver(object driver)
    {
        throw new NotImplementedException();
    }
}