using ChecklistService.Application.Extensions;
using ChecklistService.Persistence.Extensions;
using MassTransit;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using Services.Core.Events.ChecklistsEvents;
using Services.Core.Events.DriverEvents;
using Configuration = ChecklistService.Application.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddOptions<Configuration>().Bind(builder.Configuration.GetSection(nameof(Configuration)));

builder.Services.AddSingleton<CosmosClient>((serviceProvider) =>
{
    var configuration = serviceProvider.GetRequiredService<IOptions<Configuration>>().Value;

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
        var configuration = context.GetRequiredService<IOptions<Configuration>>().Value;
        
        cfg.Host(configuration.AzureServiceBusSettings.ConnectionString);
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