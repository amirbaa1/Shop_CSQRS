using Microsoft.EntityFrameworkCore;
using Store.Domain.Model;


namespace Store.Infrastructure.Data
{
    public class StoreDbContext : DbContext
    {
        public DbSet<StoreModel> storeModels { get; set; }
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

    }
}
