using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly EcommerceDbContext _dbContext;

        public ProductRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Product? Get(int entityId)
        {
            Product? product = _dbContext.Products.SingleOrDefault(x => x.ProductId == entityId);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> products = _dbContext.Products.ToList();
            return products;
        }

        public IQueryable<Product> Query(Expression<Func<Product, bool>> predicate)
        {
            var products = _dbContext.Products.Where(predicate);
            return products;
        }
    }
}