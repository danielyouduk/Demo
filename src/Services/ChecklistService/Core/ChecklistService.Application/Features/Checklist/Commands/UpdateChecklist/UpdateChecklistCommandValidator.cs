using ChecklistService.Application.Validation.BaseValidation;
using FluentValidation;

namespace ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;

public class UpdateChecklistCommandValidator : ChecklistValidator<UpdateChecklistCommand>
{
    public UpdateChecklistCommandValidator()
    {
        RuleFor(command => command.id)
            .Must(id => id != Guid.Empty)
            .WithMessage("Id must be set");
        
        RuleFor(command => command.AccountId)
            .Must(accountId => accountId != Guid.Empty)
            .WithMessage("AccountId must be set");
    }
}