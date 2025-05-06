using FleetManagementService.Application.Features.Account.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.CreateAccount;

/// <summary>
/// Represents a command for creating an account.
/// </summary>
public record CreateAccountCommand(Guid Id) : IRequest<ServiceResponse<AccountDto>>;