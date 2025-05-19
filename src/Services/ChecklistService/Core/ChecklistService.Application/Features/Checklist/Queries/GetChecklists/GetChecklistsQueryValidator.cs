using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;

namespace ChecklistService.Application.Features.Checklist.Queries.GetChecklists;

public class GetChecklistsQueryValidator : BaseValidator<GetChecklistsQuery>
{
    public GetChecklistsQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}