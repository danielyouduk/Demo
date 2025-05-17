using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAccountCommand, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(
        CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await accountRepository.CreateAsync(request, cancellationToken);
        
        // todo: Add Current User to account
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new ServiceResponse<AccountDto>
        {
            Data = account,
            Message = "Account created successfully",
            Status = ServiceStatus.Success
        };
    }
}