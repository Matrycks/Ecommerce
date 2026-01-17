
namespace Ecommerce.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; private set; } = null!;
        public string? Desc { get; private set; }
        public decimal Price { get; private set; }

        public Product() { }

        public Product(string name, string? desc, decimal price)
        {
            if (string.IsNullOrEmpty(name) || price <= 0) throw new ArgumentException("Invalid params for creating product");

            Name = name;
            Desc = desc;
            Price = price;
        }

        public void SetPrice(decimal newPrice)
        {
            if (newPrice <= 0) throw new ArgumentException("Product pricing must be greater than 0");
            Price = newPrice;
        }
    }
}