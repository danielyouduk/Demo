using System.Text.Json;
using System.Text.Json.Serialization;
using ChecklistService.Application.Settings;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Events.DriverEvents;
using Services.Core.ServiceBus;

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
        
                cfg.Message<ChecklistCreated>(x => 
                    x.SetEntityName(ServiceBusConstants.Topics.Checklist.Created));
        
                cfg.Message<ChecklistSubmitted>(x => 
                    x.SetEntityName(ServiceBusConstants.Topics.Checklist.Submitted));
                
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