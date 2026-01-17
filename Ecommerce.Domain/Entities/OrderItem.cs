
namespace Ecommerce.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal Cost { get; set; }

        public OrderItem() { }

        public OrderItem(int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            ProductId = productId;
            Quantity = quantity;
        }

        public OrderItem(CartItem cartItem)
        {
            ProductId = cartItem.ProductId;
            Quantity = cartItem.Quantity;
            Cost = cartItem.Cost;
            Total = cartItem.Total;
        }
    }
}