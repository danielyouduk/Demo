using Services.Core.Extensions;
using Services.Core.Extensions.Settings.AzureCosmosDbSettings;
using Services.Core.Extensions.Settings.AzureServiceBus;

namespace ChecklistService.Application.Settings;

public record ChecklistServiceConfiguration : IApplicationConfiguration, IAzureServiceBusSettings, IAzureCosmosDbSettings
{
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
    public required AzureCosmosDbSettings AzureCosmosDbSettings { get; init; }
}