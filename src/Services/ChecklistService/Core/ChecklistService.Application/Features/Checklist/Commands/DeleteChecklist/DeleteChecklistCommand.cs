using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public record DeleteChecklistCommand(
    Guid Id) : IRequest<ServiceResponse<Unit>>;