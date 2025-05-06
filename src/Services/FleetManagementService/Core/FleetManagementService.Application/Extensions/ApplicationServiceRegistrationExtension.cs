using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagementService.Application.Extensions;

public static class ApplicationServiceRegistrationExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}