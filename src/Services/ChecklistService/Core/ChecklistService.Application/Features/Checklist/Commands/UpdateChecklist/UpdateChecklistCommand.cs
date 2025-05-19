using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;

public record UpdateChecklistCommand : IRequest<ServiceResponse<Unit>>
{
    public Guid id { get; set; }

    public Guid AccountId { get; set; }
    
    public string? Title { get; set; }
}