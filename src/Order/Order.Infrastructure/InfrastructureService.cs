using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repository;
using Order.Infrastructure.Consumer;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;

namespace Order.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrderdbContext>(p =>
            p.UseNpgsql(configuration["ConnectionStrings:OrderConnectionString"]));

        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        
        service.AddMassTransit(x =>
        {
            x.AddConsumer<BasketQueueEventConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });

                cfg.ReceiveEndpoint(EventBusConstants.BasketQueue,
                    e =>
                    {
                        e.ConfigureConsumer<BasketQueueEventConsumer>(context);
                    });
            });
        });

        //service.AddMassTransitHostedService();

        return service;
    }
}