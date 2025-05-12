using ChecklistService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;

public class UpdateChecklistCommandHandler(IChecklistRepository repository)
    : IRequestHandler<UpdateChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateChecklistCommand request, CancellationToken cancellationToken)
    {
        await repository.UpdateChecklistAsync(request);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist updated successfully",
            Success = true
        };
    }
}