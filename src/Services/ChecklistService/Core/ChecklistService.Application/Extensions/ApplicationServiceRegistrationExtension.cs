using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ChecklistService.Application.Extensions;

public static class ApplicationServiceRegistrationExtension
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}