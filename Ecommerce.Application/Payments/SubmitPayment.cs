using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Payments
{
    public record SubmitPaymentCommand(Order Order, int PaymentCardId) : IRequest<bool>;
    public record SubmitPaymentResponse(bool IsSuccess, string Message, Guid ConfirmationNumber);
    public class SubmitPaymentHandler : IRequestHandler<SubmitPaymentCommand, bool>
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentCardRepository _paymentCardRepository;

        public SubmitPaymentHandler(IPaymentService paymentService, IPaymentRepository paymentRepository,
            IOrderRepository orderRepository, IPaymentCardRepository paymentCardRepository)
        {
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _paymentCardRepository = paymentCardRepository;
        }

        public async Task<bool> Handle(SubmitPaymentCommand request, CancellationToken cancellationToken)
        {
            var order = request.Order;
            var paymentCard = _paymentCardRepository.Get(request.PaymentCardId) ?? throw new Exception("Payment failed, no associated payment card");

            SubmitPaymentResponse resp = await _paymentService.Execute(order.Total, paymentCard);
            if (!resp.IsSuccess)
                throw new Exception(resp.Message);

            var payment = new Payment(order.CustomerId, order.OrderId, paymentCard.PaymentCardId, order.Total, resp.ConfirmationNumber);

            _paymentRepository.Add(payment);
            _paymentRepository.SaveChanges();

            return true;
        }
    }
}