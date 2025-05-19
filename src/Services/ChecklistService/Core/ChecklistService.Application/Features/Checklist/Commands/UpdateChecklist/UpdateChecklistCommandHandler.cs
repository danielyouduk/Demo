using ChecklistService.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Services.Core.Enums;
using Services.Core.Models.Service;

namespace ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;

public class UpdateChecklistCommandHandler(
    IChecklistRepository repository,
    UpdateChecklistCommandValidator validator,
    ILogger<UpdateChecklistCommandHandler> logger)
    : IRequestHandler<UpdateChecklistCommand, ServiceResponse<Unit>>
{
    public async Task<ServiceResponse<Unit>> Handle(UpdateChecklistCommand request, CancellationToken cancellationToken)
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
            await repository.UpdateChecklistAsync(request, cancellationToken);

            return new ServiceResponse<Unit>
            {
                Data = Unit.Value,
                Message = "Checklist updated successfully",
                Status = ServiceStatus.Success
            };
        }
        catch (CosmosException ex)
        {
            logger.LogError(ex, "Failed to update checklist in Cosmos DB. Status: {StatusCode}", ex.StatusCode);
            return new ServiceResponse<Unit>
            {
                Status = ServiceStatus.Failure,
                Message = "Failed to update checklist in database"
            };
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for UpdateChecklistCommandHandler.Handle
            logger.LogError(e, string.Empty, request);
            throw;
        }
    }
}