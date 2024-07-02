using Basket.Application.Features.Basket.Commands.CheckOut;
using Basket.Domain.Repository;
using Basket.Infrastructure.Consumer;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using EventBus.Messages.Common;
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
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddMassTransit(x =>
            {
                x.AddRequestClient<CheckOutHandler>();
                x.AddConsumer<UpdateProductConsumer>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });

                    config.ReceiveEndpoint(EventBusConstants.BasketQueue, ep =>
                    {
                        ep.AutoDelete = false;
                        ep.Durable = true;
                    });

                    config.ReceiveEndpoint(EventBusConstants.UpdateProductQueue,
                        ep => { ep.ConfigureConsumer<UpdateProductConsumer>(context); });
                });
            });

            return services;
        }
    }
}