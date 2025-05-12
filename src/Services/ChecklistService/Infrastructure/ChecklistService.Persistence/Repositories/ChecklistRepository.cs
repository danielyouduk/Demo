using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Features.Checklist.Commands.CreateChecklist;
using ChecklistService.Application.Features.Checklist.Commands.DeleteChecklist;
using ChecklistService.Application.Features.Checklist.Commands.SubmitChecklist;
using ChecklistService.Application.Features.Checklist.Commands.UpdateChecklist;
using ChecklistService.Application.Features.Checklist.Shared;
using Microsoft.Azure.Cosmos;

namespace ChecklistService.Persistence.Repositories;

public class ChecklistRepository(CosmosClient client) : IChecklistRepository
{
    public async Task<ChecklistDto> CreateChecklistAsync(CreateChecklistCommand checklist)
    {
        var database = client.GetDatabase("ChecklistDatabase");
        var container = database.GetContainer("Checklists");
        
        checklist.CreatedAt = DateTime.UtcNow;
        checklist.UpdatedAt = DateTime.UtcNow;
        
        var response = await container.UpsertItemAsync<CreateChecklistCommand>(
            item: checklist,
            partitionKey: new PartitionKey(checklist.AccountId.ToString())
        );
        
        return new ChecklistDto
        {
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

    public async Task SubmitChecklistAsync(SubmitChecklistCommand checklist)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteChecklistAsync(DeleteChecklistCommand checklist)
    {
        throw new NotImplementedException();
    }
}