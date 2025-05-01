using DriverService.Application.Extensions;
using DriverService.Persistence.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);

builder.Services.AddMassTransit(x =>
{
    // Add consumers
    // x.AddConsumer<GetDriversQueryHandler>();
    // x.AddConsumer<CreateDriverConsumer>();
    // x.AddConsumer<CreateDriverAccountConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        var connectionString = builder.Configuration["Azure:ServiceBus:ConnectionString"];
        cfg.Host(connectionString);

        // Configure create driver endpoint
        cfg.ReceiveEndpoint("create-driver", e =>
        {
            // e.ConfigureConsumer<CreateDriverConsumer>(context);

            // Add retry policy
            e.UseMessageRetry(r =>
            {
                r.Intervals(
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10)
                );
            });
        });

        // Configure message topology
        cfg.ConfigureEndpoints(context);

    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();
app.MapControllers();
app.Run();