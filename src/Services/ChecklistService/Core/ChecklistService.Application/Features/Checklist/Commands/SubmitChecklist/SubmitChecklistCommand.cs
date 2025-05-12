using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public record SubmitChecklistCommand : IRequest<ServiceResponse<Unit>>
{
    public Guid id { get; set; }

    public Guid AccountId { get; set; }
    
    public bool IsSubmitted { get; set; } = false;

    public DateTime SubmittedAt { get; set; }
}