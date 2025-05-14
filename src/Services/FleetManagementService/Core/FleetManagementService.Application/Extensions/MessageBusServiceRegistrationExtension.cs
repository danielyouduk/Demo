using FleetManagementService.Application.Consumers.ChecklistEvents;
using FleetManagementService.Application.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Events.DriverEvents;

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
                    x.SetEntityName("fleet-management-driver-created"));

                cfg.SubscriptionEndpoint(
                    subscriptionName: "checklist-created",
                    topicPath: "checklist-created",
                    configure: e => { e.ConfigureConsumer<ChecklistCreatedConsumer>(context); });

                cfg.SubscriptionEndpoint(
                    subscriptionName: "checklist-submitted",
                    topicPath: "checklist-submitted",
                    configure: e => { e.ConfigureConsumer<ChecklistSubmittedConsumer>(context); });
            });
        });
        
        return services;
    }
}