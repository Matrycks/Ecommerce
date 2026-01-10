using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Carts
{
    public class RemoveCartItem
    {
        private readonly ICartRepository _cartRepo;

        public RemoveCartItem(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Cart Execute(int cartId, int cartItemId)
        {
            var cartItem = _cartRepo.GetCartItem(cartItemId)
                ?? throw new Exception("Cannot remove item, item doesn't exist");

            var cart = _cartRepo.Get(cartId) ?? throw new Exception("Cannot remove item, cart doesn't exist");
            cart.RemoveItem(cartItem);

            _cartRepo.SaveChanges();

            return cart;
        }
    }
}