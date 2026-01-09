using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICartItem
    {
        int CartItemId { get; set; }
        int ProductId { get; set; }
        decimal Cost { get; set; }
        int Quantity { get; set; }
        decimal Total { get; set; }
    }
}