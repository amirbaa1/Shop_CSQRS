using EventBus.Messages.Common;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

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
            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("localhost", "/", c =>
                {
                    c.Username("guest");
                    c.Password("guest");
                });
                cfg.ReceiveEndpoint(EventBusConstants.UpdateProductQueue, ep =>
                {
                    ep.Durable = true;
                    ep.AutoDelete = false;
                    
                });
            });
        });
        
        return service;
    }
}