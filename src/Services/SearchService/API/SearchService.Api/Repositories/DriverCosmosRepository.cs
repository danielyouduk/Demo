using Azure.Data.Tables;
using SearchService.Api.Models;

namespace SearchService.Api.Repositories;

public class DriverCosmosRepository : IDriverCosmosRepository
{
    public async Task AddDriverAsync(DriverEntity driver)
    {
        string connectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";
        TableServiceClient serviceClient = new TableServiceClient(connectionString);

        // Create a table client for a specific table
        var tableClient = serviceClient.GetTableClient(tableName: "drivers");
        await tableClient.CreateIfNotExistsAsync();

        
        await tableClient.AddEntityAsync(driver);

        Console.WriteLine($"Added entity: {driver.Title}");

    }

    public async Task<ITableEntity> GetDriverByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}