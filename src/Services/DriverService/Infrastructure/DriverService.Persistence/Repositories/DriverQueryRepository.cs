using AutoMapper;
using DriverService.Application.Contracts.Persistence;
using DriverService.Application.Features.Drivers.Shared;
using DriverService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace DriverService.Persistence.Repositories;

public class DriverQueryRepository(DriverDatabaseContext context, IMapper mapper) : IDriverQueryRepository
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
}