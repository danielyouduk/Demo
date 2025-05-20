using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Driver.Commands.CreateDriver;

public class CreateDriverCommandValidator : FleetManagementValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator(AccountByIdMustExistAsync accountByIdMustExistAsync)
    {
        RuleFor(command => command.FirstName)
            .Length(0, 50)
            .WithMessage("First name must be between 0 and 50 characters");
        
        RuleFor(command => command.LastName)
            .Length(0, 50)
            .WithMessage("Last name must be between 0 and 50 characters");
        
        RuleFor(command => command.AccountId)
            .MustAsync(async (id, cancellation) => await accountByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}