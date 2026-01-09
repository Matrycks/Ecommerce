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

        public Order(int customerId, Guid orderNumber, ICollection<IOrderItem> orderItems)
        {
            if (customerId <= 0) throw new Exception("Invalid customerId");
            if (orderItems.Count <= 0) throw new Exception("Orders cannot be created without items");

            CustomerId = customerId;
            OrderNumber = orderNumber;
            CreatedDate = DateTime.Now.ToUniversalTime();
            Items = orderItems;
        }

        public void SetStatus(OrderStatus newStatus)
        {
            if (IsPaid) throw new Exception("Order is paid, status cannot be changed");

            if (newStatus == OrderStatus.Paid) IsPaid = true;

            Status = newStatus;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Shipped || Status == OrderStatus.Delivered)
                throw new Exception("Order delivered and cannot be cancelled");

            Status = OrderStatus.Cancelled;
        }
    }
}