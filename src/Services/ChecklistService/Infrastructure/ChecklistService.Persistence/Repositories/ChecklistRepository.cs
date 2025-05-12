using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Shared;
using ChecklistService.Domain.Entities;
using Microsoft.Azure.Cosmos;

namespace ChecklistService.Persistence.Repositories;

public class ChecklistRepository(CosmosClient client) : IChecklistRepository
{
    private Container GetContainer()
    {
        var database = client.GetDatabase("ChecklistDatabase");
        return database.GetContainer("Checklists");
    }

    public async Task<ChecklistDto> CreateChecklistAsync(CreateChecklistCommand checklist)
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
            partitionKey: new PartitionKey(entity.AccountId.ToString())
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

    public async Task UpdateChecklistAsync(UpdateChecklistCommand checklist)
    {
        throw new NotImplementedException();
    }

    public async Task SubmitChecklistAsync(SubmitChecklistCommand command)
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

    public async Task DeleteChecklistAsync(DeleteChecklistCommand checklist)
    {
        throw new NotImplementedException();
    }
}