using ChecklistService.Application.Validation.BaseValidation;
using FluentValidation;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public class CreateChecklistCommandValidator : ChecklistValidator<CreateChecklistCommand>
{
    public CreateChecklistCommandValidator()
    {
        RuleFor(command => command.Title)
            .Length(0, 50)
            .WithMessage("Title must be between 0 and 50 characters");
    }
}