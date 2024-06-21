using Basket.Domain.Repository;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Basket.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BasketdbContext>(op => op.UseNpgsql(configuration["ConnectionStrings:BasketConnectionString"]));


            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService>();

            //services.AddScoped<BasketService>();


            return services;
        }

    }
}
