using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.DbInitializer
{
    public static class ProductsInitializer
    {
        public static IServiceProvider Seed(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

            if (context.Products.Any()) return serviceProvider;

            context.Products.AddRange(
                new Product("Laptop", "High performance laptop", 1200.50m),
                new Product("Smartphone", "Latest Android smartphone", 799.99m),
                new Product("Headphones", "Noise-cancelling headphones", 199.99m),
                new Product("Smartwatch", "Fitness tracking smartwatch", 249.99m),
                new Product("Tablet", "10-inch display tablet", 499.99m)
            );

            context.SaveChanges();

            return serviceProvider;
        }
    }
}