using System.Text.Json;
using System.Text.Json.Serialization;
using FleetManagementService.Application.Consumers.ChecklistEvents;
using FleetManagementService.Application.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Events.DriverEvents;
using Services.Core.ServiceBus;

namespace FleetManagementService.Application.Extensions;

public static class MessageBusServiceRegistrationExtension
{
    public static IServiceCollection AddMessageBusServices(this IServiceCollection services,
        FleetManagementServiceConfiguration serviceConfiguration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<ChecklistCreatedConsumer>();
            config.AddConsumer<ChecklistSubmittedConsumer>();

            config.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(serviceConfiguration.AzureServiceBusSettings.ConnectionString);

                cfg.Message<DriverCreated>(x =>
                    x.SetEntityName(ServiceBusConstants.Topics.Driver.Created));

                cfg.SubscriptionEndpoint(
                    subscriptionName: ServiceBusConstants.Topics.Checklist.Subscriptions.FleetManagementCreated,
                    topicPath: ServiceBusConstants.Topics.Checklist.Created,
                    configure: e => { e.ConfigureConsumer<ChecklistCreatedConsumer>(context); });

                cfg.SubscriptionEndpoint(
                    subscriptionName: ServiceBusConstants.Topics.Checklist.Subscriptions.FleetManagementSubmitted,
                    topicPath: ServiceBusConstants.Topics.Checklist.Submitted,
                    configure: e => { e.ConfigureConsumer<ChecklistSubmittedConsumer>(context); });
                
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