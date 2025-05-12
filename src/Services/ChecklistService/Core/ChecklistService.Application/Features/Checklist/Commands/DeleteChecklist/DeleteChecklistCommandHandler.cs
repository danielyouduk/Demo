using ChecklistService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public class DeleteChecklistCommandHandler(IChecklistRepository checklistRepository)
    : IRequestHandler<DeleteChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
    {
        await checklistRepository.DeleteChecklistAsync(request);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist deleted successfully",
            Success = true
        };
    }
}