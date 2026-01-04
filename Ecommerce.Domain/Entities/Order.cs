using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Entities
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<IOrderItem> Items { get; set; } = null!;
        public bool IsPaid { get; set; }
        public decimal Total { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedDate { get; set; }

        public Order(int customerId, Guid orderNumber)
        {
            if (customerId <= 0) throw new Exception("Invalid customerId");

            CustomerId = customerId;
            OrderNumber = orderNumber;
            CreatedDate = DateTime.Now.ToUniversalTime();
        }

        public void SetStatus(OrderStatus newStatus)
        {
            if (IsPaid) throw new Exception("Order is paid, status cannot be changed");
            Status = newStatus;
        }

        public void AddItem(IOrderItem item)
        {
            if (IsPaid) throw new Exception("Order is paid and cannot be changed");
            if (Items.Contains(item)) return;

            Items.Add(item);

            decimal itemTotal = item.Cost * item.Quantity;
            SetTotal(Total + itemTotal);
        }

        public void RemoveItem(IOrderItem item)
        {
            if (IsPaid) throw new Exception("Order is paid and cannot be changed");

            Items.Remove(item);

            decimal itemTotal = item.Cost * item.Quantity;
            SetTotal(Total - itemTotal);
        }

        public void SetTotal(decimal total)
        {
            Total = total;
        }

        public void UpdateOrderItemQuantity(int productId, int quantity)
        {
            OrderItem? orderItem = (OrderItem?)Items.SingleOrDefault(x => x.ProductId == productId)
                ?? throw new Exception("No item found with productId");
            orderItem.UpdateQuantity(quantity);
        }

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
        }
    }
}