using ChecklistService.Application.Validation.BaseValidation;
using FleetManagementService.Application.Validation;
using Services.Core.Validation;

namespace ChecklistService.Application.Features.Checklist.Queries.GetChecklists;

public class GetChecklistsQueryValidator : ChecklistValidator<GetChecklistsQuery>
{
    public GetChecklistsQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}