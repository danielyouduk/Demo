using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using SearchService.Api.Models;

namespace SearchService.Api.Repositories;

public class DriverCosmosRepository : IDriverCosmosRepository
{
    private readonly Container _container;

    public DriverCosmosRepository(CosmosClient cosmosClient, IOptions<Settings.Configuration> configurationOptions)
    {
        if (cosmosClient == null)
            throw new ArgumentNullException(nameof(cosmosClient));

        var database = cosmosClient.GetDatabase(configurationOptions.Value.Azure.CosmosDb.DatabaseName);
        _container = database.GetContainer(configurationOptions.Value.Azure.CosmosDb.ContainerName);
    }

    
    public async Task AddDriverAsync(Driver driver)
    {
        await _container.UpsertItemAsync<Driver>(
            item: driver,
            partitionKey: new PartitionKey(driver.demo)
        );
    }

    public async Task<Driver> GetDriverByIdAsync(string id)
    {
        return await _container.ReadItemAsync<Driver>(id, new PartitionKey("demo"));
    }
}