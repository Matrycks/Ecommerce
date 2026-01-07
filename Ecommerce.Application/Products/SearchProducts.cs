using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Products
{
    public class SearchProducts
    {
        private readonly IRepository<Product> _products;

        public SearchProducts(IRepository<Product> products)
        {
            _products = products;
        }

        public IQueryable<Product> Execute(Expression<Func<Product, bool>> predicate)
        {
            return _products.Query(predicate);
        }
    }
}