using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Vehicle.Commands.CreateVehicle;

public class CreateVehicleCommandValidator : BaseValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator(AccountByIdMustExistAsync accountByIdMustExistAsync)
    {
        RuleFor(command => command.RegistrationNumber)
            .Length(0, 50)
            .WithMessage("Registration number must be between 0 and 50 characters");
        
        RuleFor(command => command.AccountId)
            .MustAsync(async (id, cancellation) => await accountByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}