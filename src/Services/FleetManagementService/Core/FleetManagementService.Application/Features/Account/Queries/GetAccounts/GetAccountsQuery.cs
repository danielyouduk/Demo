using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccounts;

public record GetAccountsQuery(
    PagedRequestQuery PagedRequestQuery) : IRequest<ServiceResponseCollection<IReadOnlyCollection<AccountDto>>>;