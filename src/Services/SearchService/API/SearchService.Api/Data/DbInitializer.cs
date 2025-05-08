using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Api.Models;

namespace SearchService.Api.Data;

public class DbInitializer
{
    public static async Task InitializeAsync(WebApplication app)
    {
        await DB.InitAsync("SearchService", 
            MongoClientSettings.FromConnectionString(
                app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Name, KeyType.Text)
            .Key(x => x.Type, KeyType.Text)
            .Key(x => x.AccountId, KeyType.Text)
            .CreateAsync();
        
        // var count = await DB.CountAsync<Item>();
        //
        // if (count == 0)
        // {
        //     Console.WriteLine("No items found. Creating some...");
        //     
        //     var itemData = await File.ReadAllTextAsync("Data/search-data.json");
        //     
        //     var options = new JsonSerializerOptions{PropertyNameCaseInsensitive = true};
        //     
        //     var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);
        //     
        //     await DB.SaveAsync(items);
        // }
    }
}