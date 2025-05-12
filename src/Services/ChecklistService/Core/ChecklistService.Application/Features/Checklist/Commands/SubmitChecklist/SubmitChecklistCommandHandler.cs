using ChecklistService.Application.Contracts.Persistence;
using FleetManagementService.Application.Contracts.Persistence.Common;
using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommandHandler(IChecklistRepository repository)
    : IRequestHandler<SubmitChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(SubmitChecklistCommand request, CancellationToken cancellationToken)
    {
        await repository.SubmitChecklistAsync(request);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist submitted successfully",
            Success = true
        };
    }
}