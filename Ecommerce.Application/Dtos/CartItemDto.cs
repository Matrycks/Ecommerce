using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Dtos
{
    public class CartItemDto
    {
        public int CartId { get; set; }
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}