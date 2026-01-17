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
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext _dbContext;

        public OrderRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Order Add(Order entity)
        {
            _dbContext.Orders.Add(entity);
            return entity;
        }

        public void Delete(int entityId)
        {
            var order = _dbContext.Orders.Find(entityId);
            if (order != null)
                _dbContext.Orders.Remove(order);
        }

        public Order? Get(int entityId)
        {
            var order = _dbContext.Orders.Find(entityId);
            return order;
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetCustomerOrders(int customerId)
        {
            IEnumerable<Order> orders = [.. _dbContext.Orders.Where(o => o.CustomerId == customerId)];
            return orders;
        }

        public IEnumerable<Order> Query(Expression<Func<Order, bool>> predicate)
        {
            return _dbContext.Orders.Where(predicate);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}