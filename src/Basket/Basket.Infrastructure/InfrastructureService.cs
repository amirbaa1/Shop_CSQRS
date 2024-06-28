using Basket.Domain.Repository;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using EventBus.Messages.Common;
using EventBus.Messages.Event;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Basket.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BasketdbContext>(op =>
                op.UseNpgsql(configuration["ConnectionStrings:BasketConnectionString"]));


            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            //services.AddScoped<BasketService>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host("localhost", "/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });
                    cfg.ReceiveEndpoint(EventBusConstants.BasketQueue, ep =>
                    {
                        ep.Durable = true;
                    });
                });
            });
            services.AddMassTransitHostedService();

            return services;
        }
    }
}