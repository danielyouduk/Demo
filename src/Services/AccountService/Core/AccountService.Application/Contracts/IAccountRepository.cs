using AccountService.Application.Features.Account.Shared;
using Services.Core.Models;

namespace AccountService.Application.Contracts;

public interface IAccountRepository
{
    Task<BasePagedResult<AccountDto>> GetAccountsAsync(PagedRequestQuery pagedRequestQuery, string userId, bool isAdmin, int? accountId = null);
    
    Task<AccountDto?> GetAccountByIdAsync(Guid id);
}