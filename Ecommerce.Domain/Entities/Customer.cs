using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Interfaces;

namespace Ecommerce.Domain.Entities
{
    public class Customer : ICustomer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string Password { get; private set; } = null!;

        public Customer(string firstname, string lastname, string email, string? password)
        {
            if (string.IsNullOrEmpty(firstname) ||
                string.IsNullOrEmpty(lastname) ||
                string.IsNullOrEmpty(email))
                throw new Exception("Invalid params creating customer");

            FirstName = firstname;
            LastName = lastname;
            Email = email;

            if (!string.IsNullOrEmpty(password)) Password = password;
        }

        public void SetPassword(string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword)) throw new Exception("Password cannot be null");

            //TODO: make sure new password doesn't match existing

            Password = newPassword;
        }
    }
}