using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using Services.Core.Validation;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicles;

public class GetVehiclesQueryValidator : FleetManagementValidator<GetVehiclesQuery>
{
    public GetVehiclesQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}