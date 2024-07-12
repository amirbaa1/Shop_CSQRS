using EventBus.Messages.Event.Product;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Consumer;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;
using Product.Infrastructure.Extensions;

namespace Product.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<ProductDbContext>(x =>
            x.UseNpgsql(configuration["ConnectionStrings:ProductConnectionString"]));

        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();

        service.AddMassTransit(x =>
        {
            //send
            x.AddRequestClient<ProductStoreEvent>();
            //get
            x.AddConsumer<UpdateProductStatusConsumer>();

            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, cfg) =>
            {
                //local
                //cfg.Host("localhost", "/", c =>
                //docker
                var rabbitMqHost = configuration["EventBusSettings:HostAddress"];
                cfg.Host(new Uri(rabbitMqHost), c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
                cfg.ConfigureEndpoints(context);

                //cfg.ReceiveEndpoint(EventBusConstants.UpdateProductQueue, ep => { });

                cfg.UseTimeout(timeConfig => { timeConfig.Timeout = TimeSpan.FromSeconds(60); });
            });
        });

       //service.MigrateDatabase<ProductDbContext>();


        return service;
    }
}