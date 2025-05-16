using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Services.Core.Enums;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommandHandler(IChecklistRepository repository, IPublishEndpoint publishEndpoint)
    : IRequestHandler<SubmitChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(SubmitChecklistCommand request, CancellationToken cancellationToken)
    {
        var submittedAt = DateTime.UtcNow;
        
        request.SubmittedAt = submittedAt;
        
        await repository.SubmitChecklistAsync(request);

        await publishEndpoint.Publish(new ChecklistSubmitted
        {
            ChecklistId = request.id,
            AccountId = request.AccountId,
            SubmittedAt = request.SubmittedAt
        }, cancellationToken);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist submitted successfully",
            Status = ServiceStatus.Success
        };
    }
}