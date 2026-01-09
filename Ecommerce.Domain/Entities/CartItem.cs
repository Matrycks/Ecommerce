using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }

        public CartItem() { }

        public CartItem(Product product, int quantity)
        {
            if (quantity <= 0)
                throw new Exception("Quantity must be greater than zero");

            ProductId = product.ProductId;
            Cost = product.Price;
            Quantity = quantity;

            UpdateTotal();
        }

        public void UpdateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            Quantity = quantity;

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            Total = Cost * Quantity;
        }
    }
}