using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Dtos
{
    public class OrderItemDto : IOrderItem
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public int OrderId { get; set; }
        public decimal Cost { get; set; }
    }
}