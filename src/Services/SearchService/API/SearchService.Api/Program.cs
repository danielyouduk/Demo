using MassTransit;
using SearchService.Api.Consumers;
using SearchService.Api.Data;
using Settings = SearchService.Api.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOptions<Settings.Configuration>().Bind(
    builder.Configuration.GetSection(nameof(Settings.Configuration)));

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DriverCreatedConsumer>();

    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Azure:ServiceBus:ConnectionString"]);

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
    await DbInitializer.InitializeAsync(app);
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