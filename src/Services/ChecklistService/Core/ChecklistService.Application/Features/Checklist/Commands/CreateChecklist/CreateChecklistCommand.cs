using ChecklistService.Application.Features.Checklist.Shared;
using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public record CreateChecklistCommand : IRequest<ServiceResponse<ChecklistDto>>
{
    public Guid id { get; set; }
    
    public Guid AccountId { get; init; }
    
    public string Title { get; init; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
}