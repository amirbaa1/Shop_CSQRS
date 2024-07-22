

using Microsoft.Extensions.DependencyInjection;
using Store.Infrastructure.Repository;
using Store.Domain.Repository;

namespace Store.Worker.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterStoreService(this IServiceCollection services)
        {
            services.AddScoped<IStoreRepository, StoreRepository>();
        }
    }
}
