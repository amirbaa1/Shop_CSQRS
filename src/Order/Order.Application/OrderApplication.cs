using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Application;

public static class OrderApplication
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());

        service.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        return service;
    }
}