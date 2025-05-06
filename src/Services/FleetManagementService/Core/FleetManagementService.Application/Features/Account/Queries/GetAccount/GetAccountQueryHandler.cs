using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccount;

public class GetAccountQueryHandler(IAccountRepository accountRepository) 
    : IRequestHandler<GetAccountQuery, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAccountByIdAsync(request.Id);

        return new ServiceResponse<AccountDto>
        {
            Data = account,
            Success = true,
            Message = "Success"
        };
    }
}