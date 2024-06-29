using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Basket.Application.Features.Basket.Commands.CheckOut;
using EventBus.Messages.Common;
using MassTransit;

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

            service.AddMassTransit(cfg =>
            {
                cfg.AddRequestClient<CheckOutHandler>();
                
                cfg.SetKebabCaseEndpointNameFormatter();
                cfg.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost","/", hostConfigurator => { });
                    
                    config.ReceiveEndpoint(EventBusConstants.BasketQueue, ep =>
                    {
                        ep.AutoDelete = false;
                        ep.Durable = true;
                    });
                });
            });


            return service;
        }
    }
}