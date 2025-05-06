using FleetManagementService.Application.Features.Driver.Consumers;
using FleetManagementService.Persistence.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<DriverCreatedConsumer>();

    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(builder.Configuration["Azure:ServiceBus:ConnectionString"]);

        // Map the DriverCreated event to the consumer
        cfg.ReceiveEndpoint("driver-created-queue", e =>
        {
            e.ConfigureConsumer<DriverCreatedConsumer>(context);
        });

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();