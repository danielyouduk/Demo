using MediatR;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommand(
    Guid ChecklistId,
    Guid AccountId) : IRequest<ServiceResponse<Unit>>;