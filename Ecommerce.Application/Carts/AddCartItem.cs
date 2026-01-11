using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record AddCartItemRequest(int CartId, CartItemDto Item);
    public record AddCartItemCommand(int CartId, CartItem Item) : IRequest<Cart>;

    public class AddCartItemHandler : IRequestHandler<AddCartItemCommand, Cart>
    {
        private readonly IRepository<Product> _productRepo;
        private readonly ICartRepository _cartRepo;

        public AddCartItemHandler(IRepository<Product> productRepo, ICartRepository cartRepo)
        {
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public Task<Cart> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            Product product = _productRepo.Get(request.Item.ProductId)
                ?? throw new Exception($"Item cannot be added, productId:{request.Item.ProductId} doesn't exist");

            var cart = _cartRepo.Get(request.CartId) ?? throw new Exception("No cart exist");
            cart.AddItem(new CartItem(product, request.Item.Quantity));

            _cartRepo.SaveChanges();

            return Task.FromResult(cart);
        }
    }
}