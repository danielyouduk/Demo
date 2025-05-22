using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;

public class SubmitChecklistCommandHandler(
    IChecklistRepository checklistRepository,
    SubmitChecklistCommandValidator validator,
    IPublishEndpoint publishEndpoint,
    ILogger<SubmitChecklistCommandHandler> logger)
    : IRequestHandler<SubmitChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(SubmitChecklistCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Success,
                Message = validationResult.Errors.First().ErrorMessage
            };

        try
        {
            var submittedAt = DateTime.UtcNow;
        
            request.SubmittedAt = submittedAt;
        
            await checklistRepository.SubmitChecklistAsync(request, cancellationToken);

            await publishEndpoint.Publish(new ChecklistSubmitted
            {
                ChecklistId = request.id,
                AccountId = request.AccountId,
                SubmittedAt = request.SubmittedAt
            }, cancellationToken);

            return new ServiceResponse<Unit>
            {
                Data = Unit.Value,
                Message = "Checklist submitted successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (CosmosException ex)
        {
            logger.LogError(ex, "Failed to submit checklist in Cosmos DB. Status: {StatusCode}", ex.StatusCode);
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Failure,
                Message = "Failed to submit checklist in database"
            };
        }
        catch (OperationCanceledException)
        {
            logger.LogInformation("Operation '{Operation}' was cancelled", nameof(Handle));
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for SubmitChecklistCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}