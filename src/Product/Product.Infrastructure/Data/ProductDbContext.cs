using Microsoft.EntityFrameworkCore;
using Product.Domain.Model;

namespace Product.Infrastructure.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {

    }
    public DbSet<Domain.Model.Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}