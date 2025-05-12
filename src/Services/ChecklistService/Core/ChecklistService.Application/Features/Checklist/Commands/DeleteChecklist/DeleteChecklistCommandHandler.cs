using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public class DeleteChecklistCommandHandler(IChecklistRepository checklistRepository, IPublishEndpoint publishEndpoint)
    : IRequestHandler<DeleteChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
    {
        await checklistRepository.DeleteChecklistAsync(request);

        await publishEndpoint.Publish(new ChecklistDeleted
        {
            ChecklistId = request.Id,
            AccountId = request.AccountId
        }, cancellationToken);

        return new ServiceResponse<Unit>
        {
            Data = Unit.Value,
            Message = "Checklist deleted successfully",
            Success = true
        };
    }
}