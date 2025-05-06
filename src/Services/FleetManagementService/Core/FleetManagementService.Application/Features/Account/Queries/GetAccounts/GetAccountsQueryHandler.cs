using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccounts;

public class GetAccountsQueryHandler(IAccountRepository accountRepository)
    : IRequestHandler<GetAccountsQuery, ServiceResponseCollection<IReadOnlyCollection<AccountDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<AccountDto>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await accountRepository.GetAccountsAsync(request.PagedRequestQuery);
        
        return new ServiceResponseCollection<IReadOnlyCollection<AccountDto>>
        {
            Data = accounts.Data,
            TotalRecords = accounts.TotalRecords,
            PageSize = request.PagedRequestQuery.PageSize,
            Message = "Accounts retrieved successfully",
            Success = true
        };
    }
}