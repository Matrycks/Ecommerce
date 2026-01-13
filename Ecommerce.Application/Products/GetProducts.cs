using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products
{
    public record GetProductsCommand() : IRequest<IEnumerable<Product>>;
    public class GetProductsHandler : IRequestHandler<GetProductsCommand, IEnumerable<Product>>
    {
        private readonly IProductRepository _products;

        public GetProductsHandler(IProductRepository products)
        {
            _products = products;
        }

        public Task<IEnumerable<Product>> Handle(GetProductsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_products.GetAll());
        }
    }
}