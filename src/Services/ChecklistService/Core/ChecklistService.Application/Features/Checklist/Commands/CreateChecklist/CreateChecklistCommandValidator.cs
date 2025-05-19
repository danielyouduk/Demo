using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public class CreateChecklistCommandValidator : BaseValidator<CreateChecklistCommand>
{
    public CreateChecklistCommandValidator(AccountByIdMustExistAsync accountByIdMustExistAsync)
    {
        RuleFor(command => command.Title)
            .Length(0, 50)
            .WithMessage("Title must be between 0 and 50 characters");
        
        RuleFor(command => command.AccountId)
            .MustAsync(async (id, cancellation) => await accountByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}