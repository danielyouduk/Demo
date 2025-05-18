using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using FluentValidation;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccount;

public class GetAccountQueryValidator : BaseValidator<GetAccountQuery>
{
    public GetAccountQueryValidator(
        AccountByIdMustExistAsync accountByIdMustExistAsync)
    {
        RuleFor(command => command.Id)
            .NotEmpty()
            .WithMessage("Account ID is required");
        
        RuleFor(command => command.Id)
            .MustAsync(async (id, cancellation) => await accountByIdMustExistAsync.ValidateAsync(id, cancellation));
    }
}