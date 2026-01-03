using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Dtos
{
    public class OrderDto : IOrder
    {
        public int OrderId { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<IOrderItem> Items { get; set; } = null!;
    }
}