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
    public class ProductRepository : IProductRepository
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

        public Product Add(Product entity)
        {
            _dbContext.Products.Add(entity);
            return entity;
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

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Products.Find(entityId);
            if (entity != null)
                _dbContext.Products.Remove(entity);
        }
    }
}