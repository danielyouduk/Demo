using AccountService.Application.Contracts;
using AccountService.Application.Features.Account.Shared;
using AccountService.Persistence.DatabaseContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Services.Core.Helpers;
using Services.Core.Models;

namespace AccountService.Persistence.Repositories;

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
        var driver = await context.Accounts.FirstOrDefaultAsync(account => account.Id == id);

        return mapper.Map<AccountDto>(driver);
    }
}