using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record GetCartCommand(int cartId) : IRequest<Cart>;

    public class GetCartHandler : IRequestHandler<GetCartCommand, Cart>
    {
        private readonly ICartRepository _cartRepo;

        public GetCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Task<Cart> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartRepo.Get(request.cartId) ?? throw new Exception("Cart not found");
            return Task.FromResult(cart);
        }
    }
}