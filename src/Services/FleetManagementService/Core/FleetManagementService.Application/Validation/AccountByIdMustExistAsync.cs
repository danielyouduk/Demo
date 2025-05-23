using FleetManagementService.Application.Contracts.Persistence;
using FleetManagementService.Application.Exceptions;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace FleetManagementService.Application.Validation;

public class AccountByIdMustExistAsync(
    IAccountRepository accountRepository,
    ILogger<AccountByIdMustExistAsync> logger) : AbstractValidator<Guid>
{
    public async Task<bool> ValidateAsync(Guid accountId, CancellationToken cancellationToken = default)
    {
        try
        {
            return await accountRepository.ExistsAsync(accountId, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(ValidateAsync));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for AccountByIdMustExistAsync.ValidateAsync
            logger.LogError(e, string.Empty, accountId);
            throw;
        }
    }
}