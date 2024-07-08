using Basket.Domain.Repository;
using Basket.Infrastructure.Consumer;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using EventBus.Messages.Common;
using EventBus.Messages.Event.Basket;
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
                //send
                x.AddRequestClient<BasketStoreEvent>();

                //get
                x.AddConsumer<UpdateProductConsumer>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });

                    //config.ReceiveEndpoint(EventBusConstants.BasketQueue, ep => { });
                    config.ConfigureEndpoints(context);
                    config.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });

                    config.ReceiveEndpoint(EventBusConstants.UpdateProductQueue,
                        ep => { ep.ConfigureConsumer<UpdateProductConsumer>(context); });
                });
            });

            return services;
        }
    }
}