using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }

        public decimal Total { get; set; }

        public bool IsPaid { get; set; }

        public DateTime CreatedDate { get; set; }
        public ICollection<CartItemDto> Items { get; set; } = [];

        public CartDto() { }
    }
}