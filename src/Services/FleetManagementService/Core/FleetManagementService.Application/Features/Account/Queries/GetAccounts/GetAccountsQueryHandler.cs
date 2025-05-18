using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccounts;

public class GetAccountsQueryHandler(
    IAccountRepository accountRepository,
    GetAccountsQueryValidator validator,
    ILogger<GetAccountsQueryHandler> logger)
    : IRequestHandler<GetAccountsQuery, ServiceResponseCollection<IReadOnlyCollection<AccountDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<AccountDto>>> Handle(GetAccountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponseCollection<IReadOnlyCollection<AccountDto>>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var accounts = await accountRepository.GetAccountsAsync(request.PagedRequestQuery, cancellationToken);
        
            return new ServiceResponseCollection<IReadOnlyCollection<AccountDto>>
            {
                Data = accounts.Data,
                TotalRecords = accounts.TotalRecords,
                PageSize = request.PagedRequestQuery.PageSize,
                Message = "Accounts retrieved successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetAccountsQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}