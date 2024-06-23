using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;


namespace Basket.Application
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            // service.AddMassTransit(x =>
            // {
            //     x.UsingRabbitMq((ctx, cfg) =>
            //     {
            //         cfg.Host("localhost", "/", c =>
            //         {
            //             c.Username("guest");
            //             c.Password("guest");
            //         });
            //         cfg.ConfigureEndpoints(ctx);
            //     }); 
            // }); 

            return service;
        }
    }
}
