using MassTransit;
using Microsoft.Extensions.Options;
using SearchService.Api.Consumers;
using SearchService.Api.Data;
using Configuration = SearchService.Api.Settings.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOptions<Configuration>().Bind(
    builder.Configuration.GetSection(nameof(Configuration)));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DriverCreatedConsumer>();

    config.UsingAzureServiceBus((context, cfg) =>
    {
        var configuration = context.GetRequiredService<IOptions<Configuration>>().Value;
        
        cfg.Host(configuration.AzureServiceBusSettings.ConnectionString);
        cfg.SubscriptionEndpoint(
            subscriptionName: "fleet-management-driver-created-search",
            topicPath:"fleet-management-driver-created", 
            configure: e =>
            {
                e.ConfigureConsumer<DriverCreatedConsumer>(context);
            });
    });
});

var app = builder.Build();

try
{
    var configuration = app.Services.GetRequiredService<IOptions<Configuration>>().Value;
    await DbInitializer.InitializeAsync(app, configuration);
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();