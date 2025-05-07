using FleetManagementService.Application.Features.Account.Commands.CreateAccount;
using FleetManagementService.Application.Features.Account.Commands.UpdateAccount;
using FleetManagementService.Application.Features.Account.Shared;
using FleetManagementService.Domain.Entities;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IAccountRepository
{
    Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<AccountDto?> GetAccountByIdAsync(Guid id);
    
    Task<AccountDto> CreateAsync(CreateAccountCommand account);
    
    Task UpdateAsync(UpdateAccountCommand account);
    
    Task AddAccountUser(Guid accountId, Guid userId);
}