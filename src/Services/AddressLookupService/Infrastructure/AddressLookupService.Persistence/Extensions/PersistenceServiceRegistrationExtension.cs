using AddressLookupService.Application.Contracts;
using AddressLookupService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AddressLookupService.Persistence.Extensions;

public static class PersistenceServiceRegistrationExtension
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped<IAddressRepository, AddressRepository>();
    }
}