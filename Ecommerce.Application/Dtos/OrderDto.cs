using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Dtos
{
    public class OrderDto : IOrder
    {
        public int OrderId { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<IOrderItem> Items { get; set; } = null!;
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}