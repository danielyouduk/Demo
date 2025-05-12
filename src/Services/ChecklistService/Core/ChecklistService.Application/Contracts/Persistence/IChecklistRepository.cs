using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Shared;

namespace ChecklistService.Application.Contracts.Persistence;

public interface IChecklistRepository
{
    Task<ChecklistDto> CreateChecklistAsync(CreateChecklistCommand checklist);

    Task UpdateChecklistAsync(UpdateChecklistCommand checklist);
    
    Task SubmitChecklistAsync(SubmitChecklistCommand checklist);
    
    Task DeleteChecklistAsync(DeleteChecklistCommand checklist);
}