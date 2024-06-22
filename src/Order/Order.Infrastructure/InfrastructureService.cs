using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repository;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;

namespace Order.Infrastructure;

public static class InfrastructureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrderdbContext>(p =>
            p.UseNpgsql(configuration["ConnectionStrings:OrderConnectionString"]));
        service.AddScoped<IOrderRepository, OrderRepository>();
        
        return service;
    }
}