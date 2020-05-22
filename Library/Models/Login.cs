using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models
{
    [Table("Logins")]
    public class Login
    {
        public Guid LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsConnected { get; set; }
    }
}
