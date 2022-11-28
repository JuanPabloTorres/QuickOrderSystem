using Library.Helpers;
using Library.Models;
using System;

namespace Library.DTO
{
    public class UserDTO
    {
        public string Address { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string StripeCustomerId { get; set; }

        public Guid UserId { get; set; }
    }
}