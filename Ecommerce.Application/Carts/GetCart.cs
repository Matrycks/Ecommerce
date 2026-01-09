using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Carts
{
    public class GetCart
    {
        private readonly IRepository<Cart> _cartRepo;

        public GetCart(IRepository<Cart> cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Cart Execute(int cartId)
        {
            var cart = _cartRepo.Get(cartId) ?? throw new Exception("Cart not found");
            return cart;
        }
    }
}