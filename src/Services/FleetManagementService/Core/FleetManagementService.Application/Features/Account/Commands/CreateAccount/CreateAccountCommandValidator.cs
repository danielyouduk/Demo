using FluentValidation;
using FluentValidation.Results;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : BaseValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Id)
    }
}

public class BaseValidator<T> : AbstractValidator<T>;