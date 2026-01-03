using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; set; }
        Guid OrderNumber { get; set; }
        public ICollection<IOrderItem> Items { get; set; }
    }
}