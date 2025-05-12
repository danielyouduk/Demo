using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Shared;
using MassTransit;
using MediatR;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public class CreateChecklistCommandHandler(IChecklistRepository checklistRepository, IPublishEndpoint publishEndpoint) 
    : IRequestHandler<CreateChecklistCommand, ServiceResponse<ChecklistDto>>
{
    public async Task<ServiceResponse<ChecklistDto>> Handle(CreateChecklistCommand request, CancellationToken cancellationToken)
    {
        request.id = Guid.NewGuid();
        
        var checklist = await checklistRepository.CreateChecklistAsync(request);

        await publishEndpoint.Publish(new ChecklistCreated
        {
            ChecklistId = checklist.Id,
            AccountId = checklist.AccountId,
            CreatedAt = checklist.CreatedAt
        }, cancellationToken);

        return new ServiceResponse<ChecklistDto>
        {
            Data = checklist,
            Message = "Checklist created successfully",
            Success = true
        };
    }
}