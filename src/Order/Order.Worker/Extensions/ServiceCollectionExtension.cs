using Common.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Domain.Repository;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;

namespace Order.Worker.Extensions;

public static class ServiceCollectionExtension
{
    public static void RegisterOrderService(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<OrderdbContext>(p =>
            p.UseNpgsql(PublicVariables.OrderConnectionString));

        // services.AddScoped<IEmailSend, EmailSend>();
        service.AddScoped<IEmailSend, EmailSend>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
    }
}