using FleetManagementService.Application.Consumers.ChecklistEvents;
using FleetManagementService.Application.Extensions;
using FleetManagementService.Persistence.Extensions;
using MassTransit;
using Microsoft.Extensions.Options;
using Services.Core.Events.DriverEvents;
using Configuration = FleetManagementService.Application.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.GetSection(nameof(Configuration))
    .Get<Configuration>();

builder.Services.AddOptions<Configuration>()
    .Bind(builder.Configuration.GetSection(nameof(Configuration)));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<ChecklistCreatedConsumer>();
    config.AddConsumer<ChecklistSubmittedConsumer>();
    
    config.UsingAzureServiceBus((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IOptions<Configuration>>().Value;
        
        cfg.Host(configuration.AzureServiceBusSettings.ConnectionString);
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
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();