using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandHandler(
    IAccountRepository accountRepository,
    CreateAccountCommandValidator validator,
    ILogger<CreateAccountCommandHandler> logger,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAccountCommand, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(
        CreateAccountCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<AccountDto>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var createdAccount = await accountRepository.CreateAsync(request, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new ServiceResponse<AccountDto>
            {
                Data = createdAccount,
                Message = "Account created successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for CreateAccountCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}