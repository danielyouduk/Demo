using Services.Core.Extensions;
using Services.Core.Extensions.Settings.AzureServiceBus;

namespace SearchService.Application.Settings;

public record SearchServiceConfiguration : IApplicationConfiguration, IAzureServiceBusSettings
{
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
}