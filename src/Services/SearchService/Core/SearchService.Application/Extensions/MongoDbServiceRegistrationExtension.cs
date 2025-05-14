using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Application.Models;
using SearchService.Application.Settings;

namespace SearchService.Application.Extensions;

public static class MongoDbServiceRegistrationExtension
{
    public static async Task<IServiceCollection> AddMongoDbServices(this IServiceCollection services,
        SearchServiceConfiguration serviceConfiguration)
    {
        await DB.InitAsync(serviceConfiguration.MongoDbSettings.DatabaseName, 
            MongoClientSettings.FromConnectionString(
                serviceConfiguration.MongoDbSettings.ConnectionString));

        await DB.Index<Item>()
            .Key(x => x.Name, KeyType.Text)
            .Key(x => x.Type, KeyType.Text)
            .Key(x => x.AccountId, KeyType.Text)
            .CreateAsync();
        
        return services;
    }
}