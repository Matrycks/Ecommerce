using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record GetCartCommand(int cartId) : IRequest<Cart>;

    public class GetCartHandler : IRequestHandler<GetCartCommand, Cart?>
    {
        private readonly ICartRepository _cartRepo;

        public GetCartHandler(ICartRepository cartRepo)
        {
            _cartRepo = cartRepo;
        }

        public Task<Cart?> Handle(GetCartCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartRepo.Get(request.cartId) ?? null;
            return Task.FromResult(cart);
        }
    }
}