using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Payments;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IPaymentService
    {
        public Task<SubmitPaymentResponse> Execute(decimal amount, PaymentCard paymentCard);
    }
}