using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        public Payment? GetOrderPayment(int orderId);
    }
}