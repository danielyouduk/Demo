using AddressLookupService.Application.Contracts;
using AddressLookupService.Application.Features.Address.Shared;
using AddressLookupService.Persistence.DatabaseContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace AddressLookupService.Persistence.Repositories;

public class AddressRepository(AddressLookupDatabaseContext context, IMapper mapper) : IAddressRepository
{
    public async Task<BasePagedResult<AddressDto>> GetAddressesAsync(PagedRequestQuery pagedRequestQuery)
    {
        // Base query
        var query = context.Addresses
            .AsQueryable();
        
        // Apply sorting
        query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
        
        // Execute the query with pagination
        var queryCount = await query.CountAsync();
        
        // Apply pagination
        var accounts = await query.ApplyPaging(pagedRequestQuery)
            .ToListAsync();
        
        // Map and return the final result
        return new BasePagedResult<AddressDto>
        {
            Data = mapper.Map<IReadOnlyCollection<AddressDto>>(accounts),
            TotalRecords = queryCount
        };
    }

    public async Task<AddressDto> GetAddressByIdAsync(Guid id)
    {
        var address = await context.Addresses.FirstOrDefaultAsync(address => address.Id == id);

        return mapper.Map<AddressDto>(address);
    }
}