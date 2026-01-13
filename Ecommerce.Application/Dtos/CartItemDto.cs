
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