using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Persistence.DatabaseContext;
using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class AccountRepository(FleetManagementDatabaseContext context, IMapper mapper) : IAccountRepository
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

    public async Task<AccountDto> CreateAsync(CreateAccountCommand account)
    {
        var entity = mapper.Map<Account>(account);
        
        await context.AddAsync(entity);
        
        return mapper.Map<AccountDto>(entity);
    }

    public Task UpdateAsync(UpdateAccountCommand account)
    {
        throw new NotImplementedException();
    }

    public Task AddAccountUser(Guid accountId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Accounts.AnyAsync(account => account.Id == id);
    }

    public async Task IncrementChecklistCreatedCount(ChecklistCreated checklistCreated)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == checklistCreated.AccountId);

        if (account != null)
        {
            account.LastChecklistCreatedAt = checklistCreated.CreatedAt;
            account.NoOfChecklists++;
            
            context.Entry(account).State = EntityState.Modified;
            context.Entry(account).Property(p => p.CreatedAt).IsModified = false;
        }
    }

    public async Task IncrementChecklistSubmittedCount(ChecklistSubmitted checklistSubmitted)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == checklistSubmitted.AccountId);

        if (account != null)
        {
            account.LastChecklistSubmittedAt = checklistSubmitted.SubmittedAt;
            account.NoOfChecklistsSubmitted++;
            
            context.Entry(account).State = EntityState.Modified;
            context.Entry(account).Property(p => p.CreatedAt).IsModified = false;
        }
    }

    public async Task UpdateLastChecklistSubmission(Guid accountId, DateTime lastChecklistSubmission)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
    }
}