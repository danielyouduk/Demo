using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;

public class GetVehiclesQueryValidator : BaseValidator<GetVehiclesQuery>
{
    public GetVehiclesQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}