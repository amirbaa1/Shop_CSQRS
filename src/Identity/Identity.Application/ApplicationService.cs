using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;

public static class ApplicationService
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        return service;
    }
    
}