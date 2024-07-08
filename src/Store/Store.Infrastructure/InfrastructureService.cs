using EventBus.Messages.Common;
using EventBus.Messages.Event.Product;
using EventBus.Messages.Event.Store;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Repository;
using Store.Infrastructure.Consumer;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repository;


namespace Store.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(x =>
                x.UseNpgsql(configuration["ConnectionStrings:StoreConnectionString"]));

            services.AddScoped<IStoreRepository, StoreRepository>();


            services.AddMassTransit(x =>
            {
                //send
                x.AddRequestClient<UpdateProductStatusEvent>();
                x.AddRequestClient<MessageCheckStoreEvent>();
                //get
                x.AddConsumer<AddProductStoreConsumer>();
                x.AddConsumer<UpdateProductStoreConsumer>();
                x.AddConsumer<BasketStoreConsumer>();
                x.AddConsumer<CheckStoreConsumer>();

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", hostConfigurator =>
                    {
                        hostConfigurator.Username("guest");
                        hostConfigurator.Password("guest");
                    });

                    config.ConfigureEndpoints(context);
                    config.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });
                    //
                    // config.ReceiveEndpoint(EventBusConstants.AddProductStore,
                    //     ep => { ep.ConfigureConsumer<AddProductStoreConsumer>(context); });
                });
            });
            return services;
        }
    }
}