using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccount;

public class GetAccountQueryHandler(
    IAccountRepository accountRepository,
    GetAccountQueryValidator validator,
    ILogger<GetAccountQueryHandler> logger) 
    : IRequestHandler<GetAccountQuery, ServiceResponse<AccountDto>>
{
    public async Task<ServiceResponse<AccountDto>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
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
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetAccountQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}