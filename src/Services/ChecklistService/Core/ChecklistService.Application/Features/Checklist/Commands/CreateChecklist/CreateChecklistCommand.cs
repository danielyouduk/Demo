using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public record CreateChecklistCommand : IRequest<ServiceResponse<Guid>>
{
    public Guid id { get; set; }
    
    public Guid AccountId { get; init; }
    
    public string Title { get; init; }

    public bool IsSubmitted { get; set; } = false;
}