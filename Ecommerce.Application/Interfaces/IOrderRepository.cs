using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        public IEnumerable<Order> GetCustomerOrders(int customerId);
    }
}