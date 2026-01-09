using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CartRepository : IRepository<Cart>
    {
        private readonly EcommerceDbContext _dbContext;

        public CartRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cart Add(Cart entity)
        {
            _dbContext.Carts.Add(entity);
            return entity;
        }

        public Cart? Get(int entityId)
        {
            var cart = _dbContext.Carts.Find(entityId);
            return cart;
        }

        public IEnumerable<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Cart> Query(Expression<Func<Cart, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}