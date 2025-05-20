using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using Services.Core.Validation;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDrivers;

public class GetDriversQueryValidator : FleetManagementValidator<GetDriversQuery>
{
    public GetDriversQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}