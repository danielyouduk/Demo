using FleetManagementService.Application.Validation;
using FleetManagementService.Application.Validation.BaseValidation;
using Services.Core.Validation;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccounts;

public class GetAccountsQueryValidator : FleetManagementValidator<GetAccountsQuery>
{
    public GetAccountsQueryValidator(
        PagedRequestQueryValidator pagedRequestQueryValidator)
    {
        
    }
}