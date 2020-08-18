using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DTO
{
    public class UserDTO
    {

        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public Gender Gender { get; set; }

        public string StripeCustomerId { get; set; }
    }
}
