using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SearchService.Application.Consumers;
using SearchService.Application.Settings;

namespace SearchService.Application.Extensions;

public static class MessageBusServiceRegistrationExtension
{
    public static IServiceCollection AddMessageBusServices(this IServiceCollection services,
        SearchServiceConfiguration serviceConfiguration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<DriverCreatedConsumer>();

            config.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(serviceConfiguration.AzureServiceBusSettings.ConnectionString);
                
                cfg.SubscriptionEndpoint(
                    subscriptionName: "fleet-management-driver-created-search",
                    topicPath:"fleet-management-driver-created", 
                    configure: e =>
                    {
                        e.ConfigureConsumer<DriverCreatedConsumer>(context);
                    });
            });
        });
        
        return services;
    }
}