using Ecommerce.Application.Dtos;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record CreateCartRequest(int CustomerId, ICollection<CartItemDto> Items);
    public record CreateCartCommand(int CustomerId, ICollection<CartItem> Items) : IRequest<Cart>;

    public class CreateCartHandler : IRequestHandler<CreateCartCommand, Cart>
    {
        private readonly ICartRepository _cartRepo;
        private readonly IRepository<Product> _productRepo;

        public CreateCartHandler(IRepository<Product> productRepo, ICartRepository cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public Task<Cart> Handle(CreateCartCommand command, CancellationToken cancellationToken)
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

            return Task.FromResult(cart);
        }
    }
}