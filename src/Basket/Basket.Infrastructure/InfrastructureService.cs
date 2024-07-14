using Basket.Domain.Repository;
using Basket.Application.Consumer;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using EventBus.Messages.Common;
using EventBus.Messages.Event.Basket;
using EventBus.Messages.Event.Store;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Basket.Infrastructure.Repository.Service;


namespace Basket.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<BasketdbContext>(op =>
                op.UseNpgsql(configuration["ConnectionStrings:BasketConnectionString"]));


            //------------ localhost Redis ---------------------//
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = configuration["CacheSettings:ConnectionString"];
            });

            //--------------------------------------------------//

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();

            services.AddMassTransit(x =>
            {
                //send
                x.AddRequestClient<BasketStoreEvent>();
                x.AddRequestClient<CheckStoreEvent>();
                //get
                x.AddConsumer<UpdateProductConsumer>();
                x.AddConsumer<MessageResultConsumer>();
                x.AddRequestClient<CheckStoreEvent>(new Uri("queue:check-store-queue"));


                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    //local
                    //cfg.Host("localhost", "/", c =>
                    //docker
                    var rabbitMqHost = configuration["EventBusSettings:HostAddress"];
                    config.Host(new Uri(rabbitMqHost), c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });

                    //config.ReceiveEndpoint(EventBusConstants.BasketQueue, ep => { });
                    config.ConfigureEndpoints(context);
                    config.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });

                    config.ReceiveEndpoint(EventBusConstants.UpdateProductQueue,
                        ep => { ep.ConfigureConsumer<UpdateProductConsumer>(context); });
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}