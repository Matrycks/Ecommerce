using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Products
{
    public class GetProducts
    {
        private readonly IRepository<Product> _products;

        public GetProducts(IRepository<Product> products)
        {
            _products = products;
        }

        public IEnumerable<Product> Execute()
        {
            return _products.GetAll();
        }
    }
}