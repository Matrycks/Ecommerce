using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
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
            var cart = _dbContext.Carts.Include(x => x.Items).SingleOrDefault(x => x.CartId == entityId);
            return cart;
        }

        public IEnumerable<Cart> GetAll()
        {
            throw new NotImplementedException();
        }

        public CartItem? GetCartItem(int cartItemId)
        {
            return _dbContext.CartItems.Find(cartItemId);
        }

        public ICollection<CartItem> GetCartItems(int cartId)
        {
            return [.. _dbContext.CartItems.Where(x => x.CartId == cartId)];
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