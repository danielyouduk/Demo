using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAccountCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        await accountRepository.UpdateAsync(request);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new ServiceResponse<Unit>
        {
            Success = true,
            Message = "Account updated successfully"
        };
    }
}