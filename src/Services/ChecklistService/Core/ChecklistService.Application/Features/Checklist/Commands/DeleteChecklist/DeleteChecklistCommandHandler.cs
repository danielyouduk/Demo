using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;

public class DeleteChecklistCommandHandler(
    IChecklistRepository checklistRepository,
    DeleteChecklistCommandValidator validator,
    ILogger<DeleteChecklistCommandHandler> logger,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<DeleteChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(DeleteChecklistCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new ServiceResponse<Unit>
                {
                    Status = ServiceStatus.Invalid,
                    Message = validationResult.Errors.First().ErrorMessage
                };           
            }
            
            await checklistRepository.DeleteChecklistAsync(request, cancellationToken);

            await publishEndpoint.Publish(new ChecklistDeleted
            {
                ChecklistId = request.Id,
                AccountId = request.AccountId
            }, cancellationToken);

            return new ServiceResponse<Unit>
            {
                Data = Unit.Value,
                Message = "Checklist deleted successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (CosmosException ex)
        {
            logger.LogError(ex, "Failed to create checklist in Cosmos DB. Status: {StatusCode}", ex.StatusCode);
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Failure,
                Message = "Failed to create checklist in database"
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for CreateChecklistCommandHandler.Handle
            logger.LogError(e, string.Empty, request.Id);
            throw;
        }
    }
}