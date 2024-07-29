using Common.Infrastructure.Helpers;
using Common.Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Product.Domain.Repositories;
using Product.Infrastructure.Data;
using Product.Infrastructure.Repository;

namespace Product.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterProductService(this IServiceCollection service)
    {
        Console.WriteLine($"---> ENV TEST :  {PublicVariables.ProductConnectionString}");

        //dbContext
        service.AddDbContext<ProductDbContext>(x =>
            x.UseNpgsql(PublicVariables.ProductConnectionString));

        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();

        service.AddScoped<ProductService>();
    }
}