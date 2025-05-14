using ChecklistService.Application.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Events.DriverEvents;

namespace ChecklistService.Application.Extensions;

public static class MessageBusServiceRegistrationExtension
{
    public static IServiceCollection AddMessageBusServices(this IServiceCollection services,
        ChecklistServiceConfiguration serviceConfiguration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host(serviceConfiguration.AzureServiceBusSettings.ConnectionString);
        
                cfg.Message<DriverCreated>(x =>
                    x.SetEntityName("fleet-management-driver-created"));
        
                cfg.Message<ChecklistCreated>(x => 
                    x.SetEntityName("checklist-created"));
        
                cfg.Message<ChecklistSubmitted>(x => 
                    x.SetEntityName("checklist-submitted"));
            });
        });
        
        return services;
    }
}