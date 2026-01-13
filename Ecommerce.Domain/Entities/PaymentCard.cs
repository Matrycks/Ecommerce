using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class PaymentCard
    {
        public int PaymentCardId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [Length(14, 14)]
        public long Number { get; set; } = 5120350100064537;

        [Required]
        public CardType Type { get; set; }

        [Required]
        public string CardHolderFirstName { get; set; } = null!;

        [Required]
        public string CardHolderLastName { get; set; } = null!;

        [Required]
        public DateOnly ExpireDate { get; set; }

        public PaymentCard() { }
        public PaymentCard(int customerId, string firstname, string lastname, long number, CardType cardType, DateOnly expireDate)
        {
            CustomerId = customerId;
            CardHolderFirstName = firstname;
            CardHolderLastName = lastname;
            Number = number;
            Type = cardType;
            ExpireDate = expireDate;
        }
    }
}