using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Carts
{
    public record CreateCartRequest(int CustomerId, ICollection<CartItemDto> Items);
    public record CreateCartCommand(int CustomerId, ICollection<CartItem> Items);

    public class CreateCart
    {
        private readonly ICartRepository _cartRepo;
        private readonly IRepository<Product> _productRepo;

        public CreateCart(IRepository<Product> productRepo, ICartRepository cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public Cart Execute(CreateCartCommand command)
        {
            var cartItems = new List<CartItem>();
            foreach (var item in command.Items)
            {
                var product = _productRepo.Get(item.ProductId);

                if (product != null)
                    cartItems.Add(new CartItem(product, item.Quantity));
            }

            var cart = new Cart(command.CustomerId, cartItems);

            _cartRepo.Add(cart);
            _cartRepo.SaveChanges();

            return cart;
        }
    }
}