using System.Text.Json;
using System.Text.Json.Serialization;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using SearchService.Application.Consumers;
using SearchService.Application.Settings;
using Services.Core.ServiceBus;

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
                    subscriptionName: ServiceBusConstants.Topics.Driver.Subscriptions.SearchService,
                    topicPath: ServiceBusConstants.Topics.Driver.Created, 
                    configure: e =>
                    {
                        e.ConfigureConsumer<DriverCreatedConsumer>(context);
                    });
                
                cfg.ConfigureJsonSerializerOptions(options =>
                {
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    return options;
                });

            });
        });
        
        return services;
    }
}