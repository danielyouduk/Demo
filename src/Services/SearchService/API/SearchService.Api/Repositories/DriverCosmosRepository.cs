using Microsoft.Azure.Cosmos;

namespace SearchService.Api.Repositories;

public class DriverCosmosRepository(
    CosmosClient cosmosClient, string databaseName, string containerName)
{
    private readonly Container _container = cosmosClient.GetContainer(databaseName, containerName);

    
    public async Task AddDriverAsync(DriverDocument driver)
    {
        await _container.CreateItemAsync(driver, new PartitionKey("/demo"));
    }

    public async Task<DriverDocument> GetDriverByIdAsync(string id)
    {
        return await _container.ReadItemAsync<DriverDocument>(id, new PartitionKey("/demo"));

    }

    public record DriverDocument
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    
    


}