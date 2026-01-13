using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Interfaces
{
    public interface IPaymentCardRepository : IRepository<PaymentCard>
    {
        public IEnumerable<PaymentCard> GetCustomerCards(int customerId);
    }
}