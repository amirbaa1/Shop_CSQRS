
using Basket.Domain.Repository;
using Basket.Infrastructure.Data;
using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Repository.Service;
using Common.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBasket(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<BasketdbContext>(op =>
               op.UseNpgsql(configuration["ConnectionStrings:BasketConnectionString"]));



            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            //service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddScoped<IBasketProductService, BasketProductService>();
            service.AddScoped<BasketService>();
        }
    }
}
