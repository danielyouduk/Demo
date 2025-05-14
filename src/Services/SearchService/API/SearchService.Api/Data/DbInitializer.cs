using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Api.Settings;
using SearchService.Application.Models;

namespace SearchService.Api.Data;

public class DbInitializer
{
    public static async Task InitializeAsync(WebApplication app, Configuration configuration)
    {
        await DB.InitAsync(configuration.MongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(
                configuration.MongoDbSettings.ConnectionString));

        await DB.Index<Item>()
            .Key(x => x.Name, KeyType.Text)
            .Key(x => x.Type, KeyType.Text)
            .Key(x => x.AccountId, KeyType.Text)
            .CreateAsync();
    }
}