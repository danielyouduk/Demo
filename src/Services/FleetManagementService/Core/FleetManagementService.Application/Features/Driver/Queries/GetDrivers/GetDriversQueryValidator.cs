using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDrivers;

public class GetDriversQueryValidator : BaseValidator<GetDriversQueryValidator>
{
    public GetDriversQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}