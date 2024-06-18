using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Product.Application;

public static class ApplicationService
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return service;
    }
}