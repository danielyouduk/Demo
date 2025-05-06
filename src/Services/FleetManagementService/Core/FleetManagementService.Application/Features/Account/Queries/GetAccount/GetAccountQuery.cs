using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Queries.GetAccount;

public record GetAccountQuery(Guid Id)
    : IRequest<ServiceResponse<AccountDto>>;