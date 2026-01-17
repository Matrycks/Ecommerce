using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record RemoveCartItemCommand(int CartId, int CartItemId) : IRequest<Cart>;
    public class RemoveCartItemHandler : IRequestHandler<RemoveCartItemCommand, Cart>
    {
        private readonly ICartRepository _cartRepo;

        public RemoveCartItemHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Task<Cart> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartRepo.Get(request.CartId)
                ?? throw new KeyNotFoundException($"Cannot remove item, cart: {request.CartId} doesn't exist");

            var cartItem = _cartRepo.GetCartItem(request.CartItemId)
                ?? throw new KeyNotFoundException($"CartItem: {request.CartItemId} doesn't exist");

            cart.RemoveItem(cartItem);

            _cartRepo.SaveChanges();

            return Task.FromResult(cart);
        }
    }
}