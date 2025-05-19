using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Shared;
using ChecklistService.Domain.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using Services.Core.Models;

namespace ChecklistService.Persistence.Repositories;

public class ChecklistRepository(
    CosmosClient client,
    ILogger<ChecklistRepository> logger) : IChecklistRepository
{
    private Container GetContainer()
    {
        var database = client.GetDatabase("ChecklistDatabase");
        return database.GetContainer("Checklists");
    }

    public async Task<BasePagedResult<ChecklistDto>> GetChecklistsAsync(PagedRequestQuery pagedRequestQuery, CancellationToken cancellationToken)
    {
        try
        {
            // todo: Implement ChecklistRepository.GetChecklistsAsync
            throw new NotImplementedException();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for ChecklistRepository.GetChecklistsAsync
            logger.LogError(e, string.Empty, pagedRequestQuery);
            throw;
        }
    }

    public async Task<ChecklistDto?> GetChecklistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            // todo: Implement ChecklistRepository.GetChecklistByIdAsync
            throw new NotImplementedException();
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception e)
        {
            // todo: Add Exception log message for ChecklistRepository.GetChecklistByIdAsync
            logger.LogError(e, string.Empty, id);
            throw;
        }
    }

    public async Task<ChecklistDto> CreateChecklistAsync(CreateChecklistCommand checklist, CancellationToken cancellationToken)
    {
        var container = GetContainer();
        var now = DateTime.UtcNow;

        var entity = new Checklist
        {
            id = checklist.id,
            AccountId = checklist.AccountId,
            Title = checklist.Title,
            CreatedAt = now,
            UpdatedAt = now,
            IsSubmitted = false
        };
        
        var response = await container.UpsertItemAsync<Checklist>(
            item: entity,
            partitionKey: new PartitionKey(entity.AccountId.ToString()),
            cancellationToken: cancellationToken
        );
        
        return new ChecklistDto
        {
            Id = response.Resource.id,
            AccountId = response.Resource.AccountId,
            Title = response.Resource.Title,
            CreatedAt = response.Resource.CreatedAt,
            UpdatedAt = response.Resource.UpdatedAt
        };

    }

    public async Task UpdateChecklistAsync(UpdateChecklistCommand checklist, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitChecklistAsync(SubmitChecklistCommand command, CancellationToken cancellationToken)
    {
        var container = GetContainer();

        try
        {
            var response = await container.ReadItemAsync<Checklist>(
                id: command.id.ToString(),
                partitionKey: new PartitionKey(command.AccountId.ToString())
            );

            var entity = response.Resource;
            
            entity.SubmittedAt = DateTime.UtcNow;
            entity.IsSubmitted = true;
            
            await container.ReplaceItemAsync(
                item: entity,
                id: entity.id.ToString(),
                partitionKey:
                new PartitionKey(entity.AccountId.ToString()));
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            throw new KeyNotFoundException($"Checklist with id {command.id} was not found.");
        }
        
    }

    public async Task DeleteChecklistAsync(DeleteChecklistCommand checklist, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}