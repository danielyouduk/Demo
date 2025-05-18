using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;
using Services.Core.Models;

namespace FleetManagementService.Application.Validation;

public class PagedRequestQueryValidator : BaseValidator<PagedRequestQuery>
{
    public PagedRequestQueryValidator()
    {
        RuleFor(pagedRequestQuery => pagedRequestQuery)
            .NotNull()
            .WithMessage("PagedRequestQuery is required");
        
        RuleFor(pagedRequestQuery => pagedRequestQuery.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0")
            .LessThanOrEqualTo(100)
            .WithMessage("Page size must be less than or equal to 100");
        
        RuleFor(pagedRequestQuery => pagedRequestQuery.PageNumber)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0");
    }
}