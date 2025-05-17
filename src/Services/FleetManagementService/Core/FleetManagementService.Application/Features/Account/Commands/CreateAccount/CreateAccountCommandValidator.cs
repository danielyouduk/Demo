using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : BaseValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        // CompanyName validation
        RuleFor(command => command.CompanyName)
            .Length(0, 50)
            .WithMessage("Company name must be between 0 and 50 characters");
        
        // CompanyVatNumber validation
        RuleFor(command => command.CompanyVatNumber)
            .Length(0, 11)
            .WithMessage("Company VAT number must be between 0 and 11 characters");
        
        // BillingAddressId validation
        RuleFor(command => command.BillingAddressId)
            .Must(billingAddressId => billingAddressId == null || billingAddressId != Guid.Empty)
            .WithMessage("BillingAddressId must not be an empty GUID");
    }
}