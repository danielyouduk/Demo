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
        
        var response = await container.UpsertItemAsync<CreateChecklistCommand>(
            item: checklist,
            partitionKey: new PartitionKey(checklist.AccountId.ToString())
        );
        
        return new ChecklistDto();
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