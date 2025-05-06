using FleetManagementService.Application.Features.Account.Shared;
using Services.Core.Models;

namespace FleetManagementService.Application.Contracts.Persistence;

public interface IAccountRepository
{
    Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery);
    
    Task<AccountDto?> GetAccountByIdAsync(Guid id);
}