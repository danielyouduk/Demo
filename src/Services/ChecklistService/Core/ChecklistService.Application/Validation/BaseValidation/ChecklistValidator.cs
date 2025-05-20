using FluentValidation;

namespace ChecklistService.Application.Validation.BaseValidation;

public class ChecklistValidator<T> : AbstractValidator<T>;