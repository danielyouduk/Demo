using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandValidator : BaseValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator(AccountByIdMustExistAsync accountByIdMustExistAsync)
    {
        RuleFor(command => command.AccountId)
            .MustAsync(async (id, cancellation) => await accountByIdMustExistAsync.ValidateAsync(id));
    }
}