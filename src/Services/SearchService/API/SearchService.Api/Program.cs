using SearchService.Application.Extensions;
using SearchService.Application.Settings;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<SearchServiceConfiguration>(builder.Configuration);

builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

builder.Services
    .AddMessageBusServices(appConfig)
    .AddMongoDbServices(appConfig)
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

await MongoDbServiceRegistrationExtension.InitializeMongoDb(appConfig);

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();