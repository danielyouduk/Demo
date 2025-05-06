using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<CreateAccountCommand, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(
        CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.CreateAsync(request);

        return new ServiceResponse<AccountDto>
        {
            Data = account,
            Message = "Account created successfully",
            Success = true
        };
    }
}