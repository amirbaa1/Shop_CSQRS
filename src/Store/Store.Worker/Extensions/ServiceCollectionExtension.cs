

using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Repository;
using Store.Domain.Repository;
using Common.Infrastructure.Service;
using Store.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Common.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;

namespace Store.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterStoreService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(x => x.UseNpgsql(configuration["ConnectionStrings:StoreConnectionString"]));

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<StoreService>();
        }
    }
}
