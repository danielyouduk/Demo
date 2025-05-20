using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Driver.Queries.GetDriver;

public class GetDriverQueryValidator : FleetManagementValidator<GetDriverQuery>
{
    public GetDriverQueryValidator(
        DriverByIdMustExistAsync driverByIdMustExistAsync)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Driver ID is required");
        
        RuleFor(command => command.Id)
            .MustAsync(async (id, cancellation) => await driverByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}