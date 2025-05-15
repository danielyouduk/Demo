using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using SearchService.Application.Contracts.Persistence;
using SearchService.Application.Settings;
using SearchService.Persistence.DatabaseContext;
using SearchService.Persistence.Repositories;

namespace SearchService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, 
        SearchServiceConfiguration serviceConfiguration)
    {
        services.AddSingleton<IMongoClient>(serviceProvider => 
            new MongoClient(serviceConfiguration.MongoDbSettings.ConnectionString));
        
        services.AddSingleton<ISearchServiceDatabaseContext, SearchServiceDatabaseContext>();
        
        services.AddScoped<ISearchRepository, SearchRepository>();
        
        return services;
    }
}
