using SearchService.Application.Extensions;
using SearchService.Application.Settings;
using SearchService.Persistence.DatabaseContext;
using SearchService.Persistence.Extensions;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<SearchServiceConfiguration>(builder.Configuration);

builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

builder.Services
    .AddApplicationServices()
    .AddMessageBusServices(appConfig)
    .AddPersistenceServices(appConfig)
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

var dbContext = app.Services.GetRequiredService<ISearchServiceDatabaseContext>();
await dbContext.InitializeAsync();


app.Run();