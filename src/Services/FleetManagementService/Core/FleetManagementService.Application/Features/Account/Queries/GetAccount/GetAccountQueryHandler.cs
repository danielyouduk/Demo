using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccount;

public class GetAccountQueryHandler(IAccountRepository accountRepository) 
    : IRequestHandler<GetAccountQuery, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.GetAccountByIdAsync(request.Id, cancellationToken);

        if (account == null)
        {
            return new ServiceResponse<AccountDto>
            {
                Status = ServiceStatus.NotFound,
                Message = $"Account with ID {request.Id} not found",
                Data = null
            };
        }

        return new ServiceResponse<AccountDto>
        {
            Status = ServiceStatus.Success,
            Message = "Account retrieved successfully",
            Data = account
        };
    }
}