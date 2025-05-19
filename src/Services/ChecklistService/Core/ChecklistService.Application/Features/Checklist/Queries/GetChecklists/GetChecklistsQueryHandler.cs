using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Queries.GetChecklists;

public class GetChecklistsQueryHandler(
    IChecklistRepository checklistRepository,
    GetChecklistsQueryValidator validator,
    ILogger<GetChecklistsQueryHandler> logger)
    : IRequestHandler<GetChecklistsQuery, ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>>
{
    public async Task<ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>> Handle(GetChecklistsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            var accounts = await checklistRepository.GetChecklistsAsync(request.PagedRequestQuery, cancellationToken);
        
            return new ServiceResponseCollection<IReadOnlyCollection<ChecklistDto>>
            {
                Data = accounts.Data,
                TotalRecords = accounts.TotalRecords,
                PageSize = request.PagedRequestQuery.PageSize,
                Message = "Accounts retrieved successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for GetAccountsQueryHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}