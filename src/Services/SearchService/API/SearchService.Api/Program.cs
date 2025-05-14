using SearchService.Application.Extensions;
using SearchService.Application.Settings;
using Services.Core.Extensions;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Services.AddApplicationConfiguration<SearchServiceConfiguration>(builder.Configuration);

builder.Services.AddSingleton(appConfig);
builder.Services.AddControllers();

await builder.Services
    .AddMessageBusServices(appConfig)
    .AddMongoDbServices(appConfig);

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();