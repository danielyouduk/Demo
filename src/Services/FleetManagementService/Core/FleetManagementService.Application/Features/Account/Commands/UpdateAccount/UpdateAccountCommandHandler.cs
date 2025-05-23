using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.UpdateAccount;

public class UpdateAccountCommandHandler(
    IAccountRepository accountRepository,
    UpdateAccountCommandValidator validator,
    ILogger<UpdateAccountCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateAccountCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<Unit>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var updated = await accountRepository.UpdateAsync(request, cancellationToken);
            if (!updated)
            {
                return new ServiceResponse<Unit>
                {
                    Status = ServiceStatus.NotFound,
                    Message = "Account not found"
                };
            }
            
            await unitOfWork.SaveChangesAsync(cancellationToken);
        
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Success,
                Message = "Account updated successfully"
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for UpdateAccountCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}