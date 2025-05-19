using ChecklistService.Application.Features.Checklist.Shared;
using MediatR;
using Services.Core.Models;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Queries.GetChecklists;

public record GetChecklistsQuery(
    PagedRequestQuery PagedRequestQuery) : IRequest<ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>>;