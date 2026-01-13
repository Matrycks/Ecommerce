using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public int PaymentCardId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public Guid ConfirmationNumber { get; set; }

        [Required]
        public DateTime PaidDate { get; set; }

        public Payment() { }
        public Payment(int customerId, int orderId, int paymentCardId,
            decimal amount, Guid confirmationNumber)
        {
            if (customerId <= 0 || orderId <= 0 || paymentCardId <= 0 || amount <= 0)
                throw new Exception("Invalid params creating payment");

            CustomerId = customerId;
            OrderId = orderId;
            PaymentCardId = paymentCardId;
            Amount = amount;
            ConfirmationNumber = confirmationNumber;
            PaidDate = DateTime.UtcNow;
        }
    }
}