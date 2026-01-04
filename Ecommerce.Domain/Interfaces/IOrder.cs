using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; set; }
        int CustomerId { get; set; }
        Guid OrderNumber { get; set; }
        decimal Total { get; }
        bool IsPaid { get; set; }
        OrderStatus Status { get; }
        DateTime CreatedDate { get; set; }
        public ICollection<IOrderItem> Items { get; set; }
    }
}