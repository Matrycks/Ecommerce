using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using MediatR;

namespace Ecommerce.Application.Products
{
    public record SearchProductsCommand(Expression<Func<Product, bool>> Query) : IRequest<IQueryable<Product>>;
    public class SearchProductsHandler : IRequestHandler<SearchProductsCommand, IQueryable<Product>>
    {
        private readonly IProductRepository _products;

        public SearchProductsHandler(IProductRepository products)
        {
            _products = products;
        }

        public IQueryable<Product> Execute(Expression<Func<Product, bool>> predicate)
        {
            return _products.Query(predicate);
        }

        public Task<IQueryable<Product>> Handle(SearchProductsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_products.Query(request.Query));
        }
    }
}