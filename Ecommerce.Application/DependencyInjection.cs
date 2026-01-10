using Ecommerce.Application.Carts;
using Ecommerce.Application.Products;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<GetProduct>();
            services.AddScoped<GetProducts>();
            services.AddScoped<CreateCart>();
            services.AddScoped<GetCart>();
            services.AddScoped<AddCartItem>();

            return services;
        }
    }
}