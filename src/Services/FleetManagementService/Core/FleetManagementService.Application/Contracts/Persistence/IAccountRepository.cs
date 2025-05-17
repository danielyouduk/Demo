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
    
    Task<AccountDto> CreateAsync(CreateAccountCommand account, CancellationToken cancellationToken);
    
    Task<bool> UpdateAsync(UpdateAccountCommand account, CancellationToken cancellationToken);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool> UpdateChecklistCreated(ChecklistCreated checklistCreated, CancellationToken cancellationToken);
    
    Task<bool> UpdateChecklistSubmitted(ChecklistSubmitted checklistSubmitted, CancellationToken cancellationToken);
}