using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Base;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICart
    {
        int CartId { get; set; }
        int CustomerId { get; set; }
        decimal Total { get; }
        bool IsPaid { get; }
        DateTime CreatedDate { get; set; }
        public ICollection<CartItemBase> Items { get; set; }
    }
}