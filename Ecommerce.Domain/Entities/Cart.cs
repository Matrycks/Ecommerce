using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Entities
{
    public class Cart
    {
        public int CartId { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; private set; }
        public bool IsPaid { get; private set; } = false;
        public DateTime CreatedDate { get; set; }
        public ICollection<CartItem> Items { get; set; } = [];

        public Cart() { }

        public Cart(int customerId, ICollection<CartItem> items)
        {
            if (customerId <= 0) throw new Exception("CustomerId must be greater than 0");

            CustomerId = customerId;
            Items = items;
            CreatedDate = DateTime.UtcNow;

            SetTotal();
        }

        public void AddItem(CartItem item)
        {
            if (IsPaid) throw new Exception("Order is paid and cannot be changed");
            if (Items.Contains(item)) return;

            Items.Add(item);

            SetTotal();
        }

        public void RemoveItem(CartItem item)
        {
            if (IsPaid) throw new Exception("Order is paid and cannot be changed");

            Items.Remove(item);

            SetTotal();
        }

        private void SetTotal()
        {
            decimal nTotal = Items.Sum(x => x.Total);
            Total = nTotal;
        }

        public void UpdateOrderItemQuantity(int productId, int quantity)
        {
            CartItem? cartItem = (CartItem?)Items.SingleOrDefault(x => x.ProductId == productId)
                ?? throw new Exception("No item found with productId");
            cartItem.UpdateQuantity(quantity);

            SetTotal();
        }

        public void Clear()
        {
            Items.Clear();

            SetTotal();
        }

        public IOrder CreateOrder(Guid orderNumber)
        {
            var orderItems = Items.Select(x => new OrderItem
            {
                ProductId = x.ProductId,
                Cost = x.Cost,
                Total = x.Total,
                Quantity = x.Quantity
            }).ToList<IOrderItem>();

            Order order = new Order(CustomerId, orderNumber, orderItems);
            return order;
        }
    }
}