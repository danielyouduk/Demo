using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public class CreateChecklistCommandHandler(IChecklistRepository checklistRepository) 
    : IRequestHandler<CreateChecklistCommand, ServiceResponse<ChecklistDto>>
{
    public async Task<ServiceResponse<ChecklistDto>> Handle(CreateChecklistCommand request, CancellationToken cancellationToken)
    {
        request.id = Guid.NewGuid();
        
        var checklist = await checklistRepository.CreateChecklistAsync(request);
        
        

        return new ServiceResponse<ChecklistDto>
        {
            Data = checklist,
            Message = "Checklist created successfully",
            Success = true
        };
    }
}