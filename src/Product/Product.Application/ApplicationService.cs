using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Features.Category.Commands;

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