using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;

namespace Ecommerce.Infrastructure.Repositories
{
    public class PaymentCardRepository : IPaymentCardRepository
    {
        private readonly EcommerceDbContext _dbContext;

        public PaymentCardRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PaymentCard Add(PaymentCard entity)
        {
            _dbContext.PaymentCards.Add(entity);
            return entity;
        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.PaymentCards.Find(entityId);
            if (entity != null)
                _dbContext.PaymentCards.Remove(entity);
        }

        public PaymentCard? Get(int entityId)
        {
            var payment = _dbContext.PaymentCards.Find(entityId);
            return payment;
        }

        public IEnumerable<PaymentCard> GetAll()
        {
            return [.. _dbContext.PaymentCards];
        }

        public IEnumerable<PaymentCard> GetCustomerCards(int customerId)
        {
            return [.. _dbContext.PaymentCards.Where(c => c.CustomerId == customerId)];
        }

        public IEnumerable<PaymentCard> Query(Expression<Func<PaymentCard, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}