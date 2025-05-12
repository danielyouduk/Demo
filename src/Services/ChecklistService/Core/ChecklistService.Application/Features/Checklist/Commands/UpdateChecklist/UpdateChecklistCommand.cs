using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;

public record UpdateChecklistCommand(
    Guid Id,
    Guid AccountId,
    string Title) : IRequest<ServiceResponse<Unit>>;