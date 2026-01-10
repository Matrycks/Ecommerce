using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;
using Ecommerce.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            string connectionName,
            bool useInMemory
        )
        {
            if (useInMemory)
                services.AddDbContext<EcommerceDbContext>(options => options.UseInMemoryDatabase("TestDb"));
            else
                services.AddDbContext<EcommerceDbContext>(options => options.UseSqlServer(connectionName));

            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<ICartRepository, CartRepository>();

            return services;
        }
    }
}