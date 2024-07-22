using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Repository;

namespace Product.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterProductService(this IServiceCollection service)
    {

   

        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}