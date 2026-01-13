using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Payments;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Infrastructure.Services
{
    public class FakePaymentService : IPaymentService
    {
        public async Task<SubmitPaymentResponse> Execute(decimal amount, PaymentCard paymentCard)
        {
            await Task.Delay(5000);

            return new SubmitPaymentResponse(true, "Payment succeeded", Guid.NewGuid());
        }
    }
}