using Order.Domain.Repository;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repository;
using Common.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Order.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void RegisterOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderdbContext>(p =>
                p.UseNpgsql(PublicVariables.OrderConnectionString));

            services.AddScoped<IEmailSend, EmailSend>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            //services.AddScoped<OrderService>();
        }
    }
}