
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.DatabaseContext
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
            : base(options) { }
    }
}