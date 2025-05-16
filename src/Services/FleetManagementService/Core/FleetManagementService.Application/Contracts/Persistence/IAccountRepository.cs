using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IAccountRepository
{
    Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken);
    
    Task<AccountDto?> GetAccountByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<AccountDto> CreateAsync(CreateAccountCommand account);
    
    Task UpdateAsync(UpdateAccountCommand account);
    
    Task AddAccountUser(Guid accountId, Guid userId);
    
    Task<bool> ExistsAsync(Guid id);
    
    Task IncrementChecklistCreatedCount(ChecklistCreated checklistCreated);
    
    Task IncrementChecklistSubmittedCount(ChecklistSubmitted checklistSubmitted);

    Task UpdateLastChecklistSubmission(Guid accountId, DateTime lastChecklistSubmission);
}