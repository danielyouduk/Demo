using FleetManagementService.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(
    IAccountRepository accountRepository,
    UpdateAccountCommandValidator validator,
    ILogger<UpdateAccountCommandHandler> logger)
    : IRequestHandler<UpdateAccountCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await validator.ValidateAsync(request, cancellationToken);
            await accountRepository.UpdateAsync(request, cancellationToken);
        
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Success,
                Message = "Account updated successfully"
            };
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for UpdateAccountCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}