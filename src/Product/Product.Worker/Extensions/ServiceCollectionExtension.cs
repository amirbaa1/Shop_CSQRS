using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Repository;

namespace Product.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterProductService(this IServiceCollection service)
    {
        service.AddSingleton<IProductRepository, ProductRepository>();
        service.AddSingleton<ICategoryRepository, CategoryRepository>();
    }
}