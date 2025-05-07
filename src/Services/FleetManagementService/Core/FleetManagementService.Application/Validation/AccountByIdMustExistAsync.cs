using FleetManagementService.Application.Contracts.Persistence;
using FluentValidation;

namespace FleetManagementService.Application.Validation;

public class AccountByIdMustExistAsync(IAccountRepository repository) : AbstractValidator<Guid>
{
    public async Task<bool> ValidateAsync(Guid accountId)
    {
        if (!await repository.ExistsAsync(accountId))
            return false;

        return true;
    }
}