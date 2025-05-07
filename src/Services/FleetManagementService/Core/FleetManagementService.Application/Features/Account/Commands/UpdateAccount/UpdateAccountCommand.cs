using MediatR;
using Services.Core.Models.Service;

namespace FleetManagementService.Application.Features.Account.Commands.UpdateAccount;

public record UpdateAccountCommand(
    Guid Id,
    string CompanyName,
    string CompanyVatNumber,
    Guid? BillingAddressId) : IRequest<ServiceResponse<Unit>>;