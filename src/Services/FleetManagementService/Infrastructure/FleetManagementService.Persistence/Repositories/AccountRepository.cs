using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Persistence.DatabaseContext;
using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class AccountRepository(AccountDatabaseContext context, IMapper mapper) : IAccountRepository
{
    public async Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery)
    {
        // Base query
        var query = context.Accounts
            .AsQueryable();
        
        // Apply sorting
        query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
        
        // Execute the query with pagination
        var queryCount = await query.CountAsync();
        
        // Apply pagination
        var accounts = await query.ApplyPaging(pagedRequestQuery)
            .ToListAsync();
        
        // Map and return the final result
        return new BasePagedResult<AccountDto>
        {
            Data = mapper.Map<IReadOnlyCollection<AccountDto>>(accounts),
            TotalRecords = queryCount
        };
    }

    public async Task<AccountDto?> GetAccountByIdAsync(Guid id)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == id);

        return mapper.Map<AccountDto>(account);
    }
}