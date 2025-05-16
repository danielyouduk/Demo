using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(IAccountRepository accountRepository)
    : IRequestHandler<UpdateAccountCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.UpdateAsync(request);
        
        return new ServiceResponse<Unit>
        {
            Status = ServiceStatus.Success,
            Message = "Account updated successfully"
        };
    }
}