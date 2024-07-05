using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Store.Domain.Repository;
using Store.Infrastructure.Data;
using Store.Infrastructure.Repository;


namespace Store.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(x => x.UseNpgsql(configuration["ConnectionStrings:StoreConnectionString"]));

            services.AddScoped<IStoreRespository, StoreRepository>();
            return services;
        }
    }
}
