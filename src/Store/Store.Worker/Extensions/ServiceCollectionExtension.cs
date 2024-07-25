

using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Repository;
using Store.Domain.Repository;
using Common.Infrastructure.Service;

namespace Store.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterStoreService(this IServiceCollection services)
        {
            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<StoreService>();
        }
    }
}
