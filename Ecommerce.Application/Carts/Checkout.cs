using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Payments;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Carts
{
    public record CheckoutRequest(int CartId, int PaymentCardId);
    public record CheckoutCommand(int CartId, int PaymentCardId) : IRequest<Order>;
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, Order>
    {
        private readonly ICartRepository _cartRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IMediator _mediator;

        public CheckoutHandler(IMediator mediator, ICartRepository cartRepository, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _cartRepo = cartRepository;
            _orderRepo = orderRepository;
        }

        public async Task<Order> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            var cart = _cartRepo.Get(request.CartId) ?? throw new KeyNotFoundException("No cart available for checkout");

            var order = Order.Create(Guid.NewGuid(), cart);

            _orderRepo.Add(order); // NOTE: status pending by default

            if (!await _mediator.Send(new SubmitPaymentCommand(order, request.PaymentCardId), cancellationToken))
                throw new Exception($"Payment failed for Order: {order.OrderNumber}");

            order.SetStatus(Domain.OrderStatus.Paid);

            // Delete cart
            _cartRepo.Delete(cart.CartId);

            // TODO: handle inventory
            // TODO: create invoice
            // TODO: send messages

            _orderRepo.SaveChanges(); // Saves transaction

            return order;
        }
    }
}