using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Products
{
    public class GetProduct
    {
        private readonly IProductRepository _products;

        public GetProduct(IProductRepository products)
        {
            _products = products;
        }

        public Product? Execute(int productId)
        {
            if (productId <= 0) throw new Exception("ProductId must be greater than zero for product retrieval");

            return _products.Get(productId);
        }
    }
}