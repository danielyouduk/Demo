using ChecklistService.Application.Settings;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace ChecklistService.Application.Extensions;

public static class CosmosDbServiceRegistrationExtension
{
    public static IServiceCollection AddCosmosDbServices(this IServiceCollection services,
        ChecklistServiceConfiguration serviceConfiguration)
    {
        services.AddSingleton<CosmosClient>((serviceProvider) =>
        {
            CosmosClient client = new(
                serviceConfiguration.AzureCosmosDbSettings.AccountEndpoint, 
                serviceConfiguration.AzureCosmosDbSettings.AccountKey
            );
    
            return client;
        });
        
        return services;
    }
}