using FleetManagementService.Application.Consumers.ChecklistEvents;
using FleetManagementService.Application.Extensions;
using FleetManagementService.Persistence.Extensions;
using MassTransit;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Events.DriverEvents;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ChecklistCreatedConsumer>();
    config.AddConsumer<ChecklistSubmittedConsumer>();
    
    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Azure:ServiceBus:ConnectionString"]);

        cfg.Message<DriverCreated>(x =>
            x.SetEntityName("fleet-management-driver-created"));
        
        cfg.SubscriptionEndpoint(
            subscriptionName: "checklist-created",
            topicPath: "checklist-created",
            configure: e =>
            {
                e.ConfigureConsumer<ChecklistCreatedConsumer>(context);
            });
        
        cfg.SubscriptionEndpoint(
            subscriptionName: "checklist-submitted",
            topicPath: "checklist-submitted",
            configure: e =>
            {
                e.ConfigureConsumer<ChecklistSubmittedConsumer>(context);
            });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();