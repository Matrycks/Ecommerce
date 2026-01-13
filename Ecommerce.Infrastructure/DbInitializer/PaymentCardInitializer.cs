using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.DatabaseContext;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.DbInitializer
{
    public static class PaymentCardInitializer
    {
        public static IServiceProvider Seed(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<EcommerceDbContext>();

            if (context.PaymentCards.Any()) return serviceProvider;

            context.PaymentCards.AddRange(
                new PaymentCard(1, "A", "Thomas", 5120350100064537, Domain.CardType.Debit, DateOnly.FromDayNumber(2)),
                new PaymentCard(1, "B", "Thomas", 5120350100061234, Domain.CardType.Credit, DateOnly.FromDayNumber(2))
            );
            context.SaveChanges();

            return serviceProvider;
        }
    }
}