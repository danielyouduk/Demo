using ChecklistService.Application.Validation.BaseValidation;
using FluentValidation;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public class DeleteChecklistCommandValidator : ChecklistValidator<DeleteChecklistCommand>
{
    public DeleteChecklistCommandValidator()
    {
        RuleFor(command => command.Id)
            .Must(id => id != Guid.Empty)
            .WithMessage("Id must be set");
        
        RuleFor(command => command.AccountId)
            .Must(accountId => accountId != Guid.Empty)
            .WithMessage("AccountId must be set");
    }
}