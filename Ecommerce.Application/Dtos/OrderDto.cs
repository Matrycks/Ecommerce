
using Ecommerce.Domain;

namespace Ecommerce.Application.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public Guid OrderNumber { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = null!;
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public bool IsPaid { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}