using Ecom.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Data
{
    public class MyDbContext : IdentityDbContext<IdentityUser>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship between Product and Stock
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Stock)
                .WithOne(s => s.Product)
                .HasForeignKey(s => s.ProductId);
        }
    }
}
