using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products
{
    public record GetProductCommand(int ProductId) : IRequest<Product>;
    public class GetProductHandler : IRequestHandler<GetProductCommand, Product?>
    {
        private readonly IProductRepository _products;

        public GetProductHandler(IProductRepository products)
        {
            _products = products;
        }

        public Task<Product?> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductId <= 0) throw new Exception("ProductId must be greater than zero for product retrieval");

            return Task.FromResult(_products.Get(request.ProductId));
        }
    }
}