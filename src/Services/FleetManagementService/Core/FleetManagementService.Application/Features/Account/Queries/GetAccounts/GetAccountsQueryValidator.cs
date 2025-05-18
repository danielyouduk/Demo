using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccounts;

public class GetAccountsQueryValidator : BaseValidator<GetAccountsQuery>
{
    public GetAccountsQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}