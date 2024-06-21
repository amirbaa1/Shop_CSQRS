using Basket.Domain.Model;
using Basket.Domain.Model.Dto;
using Microsoft.EntityFrameworkCore;


namespace Basket.Infrastructure.Data
{
    public class BasketdbContext : DbContext
    {
        public BasketdbContext(DbContextOptions<BasketdbContext> options) : base(options)
        {
        }
        public DbSet<BasketModel> baskets { get; set; }
        public DbSet<BasketItem> basketItems { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
