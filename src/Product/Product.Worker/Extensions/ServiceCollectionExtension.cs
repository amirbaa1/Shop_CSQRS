using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

namespace Product.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterProductService(this IServiceCollection service)
    {

        //dbContext
        //service.AddDbContext<ProductDbContext>(x => x.UseNpgsql(configuration["ConnectionStrings:ProductConnectionString"]));


        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}