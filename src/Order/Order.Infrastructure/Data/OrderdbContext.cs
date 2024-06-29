using Microsoft.EntityFrameworkCore;
using Order.Domain.Model;

namespace Order.Infrastructure.Data;

public class OrderdbContext : DbContext
{
    public OrderdbContext(DbContextOptions<OrderdbContext> options) : base(options)
    {
        
    }

    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }
    public DbSet<Product> Products { get; set; }
}