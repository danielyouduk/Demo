using ChecklistService.Application.Contracts.Persistence;
using ChecklistService.Application.Settings;
using ChecklistService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ChecklistService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        ChecklistServiceConfiguration configuration)
    {
        services.AddScoped<IChecklistRepository, ChecklistRepository>();

        return services;
    }
}