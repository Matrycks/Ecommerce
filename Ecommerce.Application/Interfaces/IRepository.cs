using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ecommerce.Application.Interfaces
{
    //TODO: implement async
    public interface IRepository<T> where T : class
    {
        public IEnumerable<T> GetAll();
        public T? Get(int entityId);
        public T Add(T entity);
        public IQueryable<T> Query(Expression<Func<T, bool>> predicate);
        public void SaveChanges();
    }
}