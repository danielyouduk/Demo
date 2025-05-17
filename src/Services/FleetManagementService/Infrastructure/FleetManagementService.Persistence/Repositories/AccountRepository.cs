using System.Data.Common;
using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Persistence.DatabaseContext;
using AutoMapper;
using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Helpers;
using Services.Core.Models;

namespace FleetManagementService.Persistence.Repositories;

public class AccountRepository(
    FleetManagementDatabaseContext context,
    ILogger<AccountRepository> logger,
    IMapper mapper) : IAccountRepository
{
    public async Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(pagedRequestQuery);

        try
        {
            // Base query
            var query = context.Accounts.AsQueryable();

            // Apply sorting with validation
            if (!string.IsNullOrEmpty(pagedRequestQuery.SortBy))
            {
                query = OrderingHelper.ApplyOrdering(query, pagedRequestQuery.SortBy, pagedRequestQuery.SortDescending);
            }

            // Execute the query with pagination
            var queryCount = await query.CountAsync(cancellationToken);

            if (queryCount == 0)
            {
                return new BasePagedResult<AccountDto>
                {
                    Data = Array.Empty<AccountDto>(),
                    TotalRecords = 0
                };
            }

            // Apply pagination with safety limit
            var accounts = await query
                .ApplyPaging(pagedRequestQuery)
                .ToListAsync(cancellationToken);

            // Map and return the final result
            var mappedAccounts = mapper.Map<IReadOnlyCollection<AccountDto>>(accounts);

            return new BasePagedResult<AccountDto>
            {
                Data = mappedAccounts,
                TotalRecords = queryCount
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for AccountRepository.GetAccountsAsync
            logger.LogError(e, string.Empty, pagedRequestQuery);
            throw;
        }
    }

    public async Task<AccountDto?> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (id == Guid.Empty)
        {
            // todo: Add ArgumentException message for AccountRepository.GetAccountByIdAsync
            throw new ArgumentException(string.Empty, nameof(id));
        }

        try
        {
            var account = await context.Accounts.AsNoTracking()
                .FirstOrDefaultAsync(account => account.Id == id, cancellationToken);

            return account != null ? mapper.Map<AccountDto>(account) : null;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for AccountRepository.GetAccountByIdAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }

    public async Task<AccountDto> CreateAsync(CreateAccountCommand account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        try
        {
            var entity = mapper.Map<Account>(account);
        
            await context.AddAsync(entity, cancellationToken);
        
            return mapper.Map<AccountDto>(entity);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for AccountRepository.CreateAsync
            logger.LogError(e, string.Empty, account.CompanyName);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(UpdateAccountCommand account, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(account);

        try
        {
            var existingAccount = await context.Set<Account>()
                .FirstOrDefaultAsync(a => a.Id == account.Id, cancellationToken);
            
            if (existingAccount == null)
            {
                return false;
            }

            mapper.Map(account, existingAccount);
            context.Update(existingAccount);

            return true;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for AccountRepository.UpdateAsync
            logger.LogError(e, string.Empty, account.Id, account.CompanyName);
            throw;
        }
    }

    public Task AddAccountUser(Guid accountId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await context.Accounts.AnyAsync(account => account.Id == id);
    }

    public async Task<bool> IncrementChecklistCreatedCount(ChecklistCreated checklistCreated, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(checklistCreated);
        
        try
        {
            var existingAccount = await context.Set<Account>()
                .FirstOrDefaultAsync(a => a.Id == checklistCreated.AccountId, cancellationToken);
            
            if (existingAccount == null)
            {
                return false;
            }
            
            existingAccount.LastChecklistCreatedAt = checklistCreated.CreatedAt;
            existingAccount.NoOfChecklists++;
        
            context.Entry(existingAccount).State = EntityState.Modified;
            context.Entry(existingAccount).Property(p => p.CreatedAt).IsModified = false;

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            // todo: Add Exception log message for AccountRepository.IncrementChecklistCreatedCount
            logger.LogError(ex, string.Empty, checklistCreated.AccountId);
            throw;
        }
    }

    public async Task<bool> IncrementChecklistSubmittedCount(ChecklistSubmitted checklistSubmitted, CancellationToken cancellationToken = default)
    {
        try
        {
            var existingAccount = await context.Set<Account>()
                .FirstOrDefaultAsync(a => a.Id == checklistSubmitted.AccountId, cancellationToken);
            
            if (existingAccount == null)
            {
                return false;
            }
            
            existingAccount.LastChecklistSubmittedAt = checklistSubmitted.SubmittedAt;
            existingAccount.NoOfChecklistsSubmitted++;
            
            context.Entry(existingAccount).State = EntityState.Modified;
            context.Entry(existingAccount).Property(p => p.CreatedAt).IsModified = false;

            return true;
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception ex)
        {
            // todo: Add Exception log message for AccountRepository.IncrementChecklistSubmittedCount
            logger.LogError(ex, string.Empty, checklistSubmitted.AccountId);
            throw;
        }
    }

    public async Task UpdateLastChecklistSubmission(Guid accountId, DateTime lastChecklistSubmission)
    {
        var account = await context.Accounts.FirstOrDefaultAsync(account => account.Id == accountId);
    }
}

public static class AccountErrorMessages
{
    // Database-related messages
    public const string DatabaseError = "Database error while {0}";
    public const string DatabaseErrorDetails = "Database error while {0} for account {1}";
    
    // Operation-specific messages
    public const string IncrementChecklistError = "incrementing checklist count";
    public const string UpdateAccountError = "updating account";
    public const string CreateAccountError = "creating account";
    public const string DeleteAccountError = "deleting account";
    
    // Generic error messages
    public const string UnexpectedError = "An unexpected error occurred while {0}";
    public const string ValidationError = "Validation failed while {0}";
    
    // Specific failure messages
    public const string ChecklistSubmittedFailure = "Failed to increment checklist submitted count";
    public const string ChecklistCreatedFailure = "Failed to increment checklist count";
}

