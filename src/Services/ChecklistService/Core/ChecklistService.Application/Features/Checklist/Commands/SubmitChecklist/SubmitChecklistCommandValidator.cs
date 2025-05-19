using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommandValidator : BaseValidator<SubmitChecklistCommand>
{
    public SubmitChecklistCommandValidator()
    {
        RuleFor(command => command.id)
            .Must(id => id != Guid.Empty)
            .WithMessage("Id must be set");
        
        RuleFor(command => command.AccountId)
            .Must(accountId => accountId != Guid.Empty)
            .WithMessage("AccountId must be set");
    }
}