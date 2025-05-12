using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public record DeleteChecklistCommand(
    Guid Id,
    Guid AccountId) : IRequest<ServiceResponse<Unit>>;