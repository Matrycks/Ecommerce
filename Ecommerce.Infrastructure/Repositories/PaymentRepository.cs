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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly EcommerceDbContext _dbContext;

        public PaymentRepository(EcommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Payment Add(Payment entity)
        {
            _dbContext.Payments.Add(entity);
            return entity;
        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Payments.Find(entityId);
            if (entity != null)
                _dbContext.Payments.Remove(entity);
        }

        public Payment? Get(int entityId)
        {
            var payment = _dbContext.Payments.Find(entityId);
            return payment;
        }

        public IEnumerable<Payment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Payment? GetOrderPayment(int orderId)
        {
            var payment = _dbContext.Payments.SingleOrDefault(p => p.OrderId == orderId);
            return payment;
        }

        public IEnumerable<Payment> Query(Expression<Func<Payment, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}