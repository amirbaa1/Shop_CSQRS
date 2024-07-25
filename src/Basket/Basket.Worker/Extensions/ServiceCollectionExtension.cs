
using Basket.Domain.Repository;
using Basket.Infrastructure.Repository;
using Basket.Infrastructure.Repository.Service;
using Common.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterBasket(this IServiceCollection service)
        {
            service.AddScoped<IBasketRepository, BasketRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            //service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddScoped<IBasketProductService, BasketProductService>();
            service.AddScoped<BasketService>();
        }
    }
}
