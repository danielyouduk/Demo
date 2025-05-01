using AutoMapper;
using DriverService.Application.Contracts.Persistence;
using DriverService.Application.Features.Drivers.Shared;
using DriverService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Services.Core;
using Services.Core.Helpers;
using Services.Core.Models;

namespace DriverService.Persistence.Repositories;

public class DriverQueryRepository(DriverDatabaseContext context, IMapper mapper) : IDriverQueryRepository
{
    public async Task<BasePagedResult<DriverDto>> GetDriversAsync(PagedRequestQuery paginationParameters)
    {
        // Base query
        var query = context.Drivers
            .AsQueryable();
        
        // Apply sorting
        query = OrderingHelper.ApplyOrdering(query, paginationParameters.SortBy, paginationParameters.SortDescending);
        
        // Execute the query with pagination
        var queryCount = await query.CountAsync();
        
        // Apply pagination
        var vehicles = await query.ApplyPaging(paginationParameters)
            .ToListAsync();
        
        // Map and return the final result
        return new BasePagedResult<DriverDto>
        {
            Data = mapper.Map<IReadOnlyCollection<DriverDto>>(vehicles),
            TotalRecords = queryCount
        };
    }

    public Task<object> GetDriverById(Guid id)
    {
        throw new NotImplementedException();
    }
}