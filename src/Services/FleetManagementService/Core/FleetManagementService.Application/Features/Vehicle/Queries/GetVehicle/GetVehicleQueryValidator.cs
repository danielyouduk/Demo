using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Vehicle.Queries.GetVehicle;

public class GetVehicleQueryValidator : BaseValidator<GetVehicleQuery>
{
    public GetVehicleQueryValidator(
        VehicleByIdMustExistAsync vehicleByIdMustExistAsync)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Vehicle ID is required");
        
        RuleFor(command => command.Id)
            .MustAsync(async (id, cancellation) => await vehicleByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}