using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommandHandler(IChecklistRepository repository, IPublishEndpoint publishEndpoint)
    : IRequestHandler<SubmitChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(SubmitChecklistCommand request, CancellationToken cancellationToken)
    {
        await repository.SubmitChecklistAsync(request);

        await publishEndpoint.Publish(new ChecklistSubmitted
        {
            ChecklistId = request.ChecklistId,
            AccountId = request.AccountId
        }, cancellationToken);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist submitted successfully",
            Success = true
        };
    }
}