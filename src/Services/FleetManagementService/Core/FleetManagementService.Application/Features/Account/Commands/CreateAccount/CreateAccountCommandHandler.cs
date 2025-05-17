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
            await validator.ValidateAsync(request, cancellationToken);
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
        catch (Exception e)
        {
            // todo: Add Exception log message for CreateAccountCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}