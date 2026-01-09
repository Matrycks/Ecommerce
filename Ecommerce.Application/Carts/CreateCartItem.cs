using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Base;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Application.Carts
{
    public record CreateCartItemRequest(int CartId, CartItemBase Item);
    public record CreateCartItemCommand(int CartId, CartItem Item);

    public class CreateCartItem
    {
        private readonly IRepository<Cart> _cartRepo;

        public CreateCartItem(IRepository<Cart> cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Cart Execute(CreateCartItemCommand request)
        {
            var cart = _cartRepo.Get(request.CartId) ?? throw new Exception("No cart exist");
            cart.AddItem(request.Item);

            _cartRepo.SaveChanges();

            return cart;
        }
    }
}