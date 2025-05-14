using System.Configuration;
using ChecklistService.Application.Extensions;
using ChecklistService.Application.Settings;
using ChecklistService.Persistence.Extensions;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Events.DriverEvents;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<ChecklistServiceConfiguration>(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(appConfig);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<Configuration>().Bind(builder.Configuration.GetSection(nameof(Configuration)));

builder.Services.AddSingleton<CosmosClient>((serviceProvider) =>
{
    var configuration = serviceProvider.GetRequiredService<ChecklistServiceConfiguration>();

    CosmosClient client = new(
        configuration.AzureCosmosDbSettings.AccountEndpoint, 
        configuration.AzureCosmosDbSettings.AccountKey
    );
    return client;
});

builder.Services.AddMassTransit(config =>
{
    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(context.GetRequiredService<ChecklistServiceConfiguration>()
            .AzureServiceBusSettings.ConnectionString);
        
        cfg.Message<DriverCreated>(x =>
            x.SetEntityName("fleet-management-driver-created"));
        
        cfg.Message<ChecklistCreated>(x => 
            x.SetEntityName("checklist-created"));
        
        cfg.Message<ChecklistSubmitted>(x => 
            x.SetEntityName("checklist-submitted"));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();