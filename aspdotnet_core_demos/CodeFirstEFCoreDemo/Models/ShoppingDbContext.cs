using Microsoft.EntityFrameworkCore;

namespace CodeFirstEFCoreDemo.Models
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            :base(options)
        {
                
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItem { get; set; }
    }
}
