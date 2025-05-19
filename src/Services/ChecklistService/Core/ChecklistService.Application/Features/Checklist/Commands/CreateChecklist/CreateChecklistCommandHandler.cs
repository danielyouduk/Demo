using ChecklistService.Application.Contracts.Persistence;
using MassTransit;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;

public class CreateChecklistCommandHandler(
    CreateChecklistCommandValidator validator,
    IChecklistRepository checklistRepository,
    ILogger<CreateChecklistCommandHandler> logger,
    IPublishEndpoint publishEndpoint) 
    : IRequestHandler<CreateChecklistCommand, ServiceResponse<Guid>>
{
    public async Task<ServiceResponse<Guid>> Handle(CreateChecklistCommand command, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        
        if (!validationResult.IsValid)
            return new ServiceResponse<Guid>
            {
                Status = ServiceStatus.Success,
                Message = validationResult.Errors.First().ErrorMessage
            };

        try
        {
            command.id = Guid.NewGuid();
        
            var checklist = await checklistRepository.CreateChecklistAsync(command, cancellationToken);

            await publishEndpoint.Publish(new ChecklistCreated
            {
                ChecklistId = checklist.Id,
                AccountId = checklist.AccountId,
                CreatedAt = checklist.CreatedAt
            }, cancellationToken);

            return new ServiceResponse<Guid>
            {
                Data = checklist.Id,
                Message = "Checklist created successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (CosmosException ex)
        {
            logger.LogError(ex, "Failed to create checklist in Cosmos DB. Status: {StatusCode}", ex.StatusCode);
            return new ServiceResponse<Guid>
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
            logger.LogError(e, string.Empty, command);
            throw;
        }
    }
}