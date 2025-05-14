using Services.Core.Extensions;
using Services.Core.Extensions.Settings.AzureServiceBus;
using Services.Core.Extensions.Settings.PostgresSql;

namespace FleetManagementService.Application.Settings;

public record FleetManagementConfiguration : IApplicationConfiguration, IAzureServiceBusSettings, IPostgresSqlSettings
{
    public required AzureServiceBusSettings AzureServiceBusSettings { get; init; }
    public required PostgresSqlSettings PostgresSqlSettings { get; init; }
}
