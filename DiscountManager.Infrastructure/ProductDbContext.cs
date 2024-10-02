using DiscountManager.Infrastructure.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DiscountManager.Infrastructure
{
    public class ProductDbContext:DbContext
    {
        public DbSet<Product> ProductsAll { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options): base(options) { }
    }
}
