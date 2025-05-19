using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Shared;
using Services.Core.Models;

namespace ChecklistService.Application.Contracts.Persistence;

public interface IChecklistRepository
{
    Task<BasePagedResult<ChecklistDto>> GetChecklistsAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken);
    
    Task<ChecklistDto?> GetChecklistByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<ChecklistDto> CreateChecklistAsync(CreateChecklistCommand checklist, CancellationToken cancellationToken);

    Task UpdateChecklistAsync(UpdateChecklistCommand checklist, CancellationToken cancellationToken);
    
    Task SubmitChecklistAsync(SubmitChecklistCommand checklist, CancellationToken cancellationToken);
    
    Task DeleteChecklistAsync(DeleteChecklistCommand checklist, CancellationToken cancellationToken);
}